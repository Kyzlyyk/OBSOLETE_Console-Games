using static System.Console;
using ToolBar;
using System;

namespace FlappyBird;

internal sealed class GameController
{
    public GameController(int gameSpeed = 150)
    {
        _gameSpeed = gameSpeed;
    }

    private int _gameSpeed;

    private const int _backgroundHeight = 3;

    private const byte _birdSize = 3;

    private const int MaxBarrierWidth = 10;
    private readonly static int MaxBarrierHeight = (WindowHeight / 2 - _backgroundHeight) + _birdSize / 2;

    private Bird _bird = new(WindowWidth / 2, WindowHeight / 2, color: ConsoleColor.Magenta, birdSize: _birdSize);

    private List<Barrier> _barriers = new();
    private ConsoleColor _barrierColor = ConsoleColor.DarkCyan;
    
    private int _score = 0;
    private List<int> _records = new();

    private Thread _playSongGameOver = new(PlaySongGameOver);
    private Thread _playSongCheckPoint = new(PlaySongCheckPoint);

    private static void DrawMap()
    {
        int y = WindowHeight - 3;
        int x = 0;

        ForegroundColor = ConsoleColor.DarkGreen;

        for (int i = 0; i < WindowWidth; i++)
        {
            SetCursorPosition(x++, y);

            Write("■");
        }

        x = 0;
        y = WindowHeight - 2;

        ForegroundColor = ConsoleColor.DarkGray;

        for (int i = 0; i < _backgroundHeight - 1; i++)
        {
            for (int j = 0; j < WindowWidth; j++)
            {
                SetCursorPosition(x++, y);

                Write("█");
            }
            y++;
            x = 0;
        }
    }

    private  static byte[,] RemoveRowFrom2DArray(byte[,] array, int rowsQty, int columnsQty, int index)
    {
        byte[,] newArray = new byte[rowsQty - 1, columnsQty];

        for (int i = 0; i < index; i++)
        {
            for (int j = 0; j < columnsQty; j++)
            {
                newArray[i, j] = array[i, j];
            }
        }

        for (int i = index; i < rowsQty - 1; i++)
        {
            for (int j = 0; j < columnsQty; j++)
            {
                newArray[i, j] = array[i + 1, j];
            }
        }

        return newArray;
    }

    private static byte[,] AddRowTo2DArray(byte[,] array, int rowsQty, int columnsQty, int index, byte[] values)
    {
        if (values.Length < columnsQty)
            throw new ArgumentException("values length must not be less or higher columnsQty");

        byte[,] newArray = new byte[rowsQty + 1, columnsQty];

        for (int i = 0; i < columnsQty; i++)
        {
            newArray[index, i] = values[i];
        }

        for (int i = 0; i < index; i++)
        {
            for (int j = 0; j < columnsQty; j++)
            {
                newArray[i, j] = array[i, j];
            }
        }

        for (int i = index; i < rowsQty; i++)
        {
            for (int j = 0; j < columnsQty; j++)
            {
                newArray[i + 1, j] = array[i, j];
            }
        }

        return newArray;
    }

    private void MoveBarriersForward()
    {
        for (int i = _barriers.Count - 1; i >= 0; i--)
        {
            for (int j = 0, k = _barriers[i].Width - 1; j < _barriers[i].Height; j++, k += _barriers[i].Width)
            {
                SetCursorPosition(_barriers[i].Coordinates[k, 0], _barriers[i].Coordinates[k, 1]);

                Write(" ");
            }

            for (int j = 0, k = _barriers[i].Width - 1; j < _barriers[i].Height - 1; j++, k += _barriers[i].Width - 1)
            {
                _barriers[i].Coordinates = RemoveRowFrom2DArray(
                    array: _barriers[i].Coordinates,
                    rowsQty: _barriers[i].Width * _barriers[i].Height - j,
                    columnsQty: 2,
                    index: k);
            }

            for (int j = 0, k = 0; j < _barriers[i].Height; j++, k += _barriers[i].Width)
            {
                _barriers[i].Coordinates = AddRowTo2DArray(
                    array: _barriers[i].Coordinates,
                    rowsQty: _barriers[i].Coordinates.Length / 2,
                    columnsQty: 2,
                    index: k,
                    values: new byte[] { (byte)(_barriers[i].Coordinates[k, 0] - 1), _barriers[i].Coordinates[k, 1] });
            }

            for (int j = 0, k = 0; j < _barriers[i].Height; j++, k += _barriers[i].Width)
            {
                SetCursorPosition(_barriers[i].Coordinates[k, 0], _barriers[i].Coordinates[k, 1]);

                ForegroundColor = _barrierColor;

                Write("■");
            }
        }
    }

    private void SpawnBarriers()
    {
        int x = WindowWidth - MaxBarrierWidth;

        byte[] randomHeights = { (byte)(MaxBarrierHeight / 2), (byte)(MaxBarrierHeight) };

        // random height of barrier
        int barrierHeight = randomHeights[new Random().Next(randomHeights.Length)];

        _barriers.Add(
            new Barrier(
                initialX: x,
                initialY: WindowTop,
                width: MaxBarrierWidth,
                height: barrierHeight,
                _barrierColor));

        if (barrierHeight >= MaxBarrierHeight)
            barrierHeight = MaxBarrierHeight / 2;

        else if (barrierHeight <= MaxBarrierHeight / 2)
            barrierHeight = MaxBarrierHeight;

        _barriers.Add(
            new Barrier(
                initialX: x,
                initialY: (WindowHeight - barrierHeight) - _backgroundHeight,
                width: MaxBarrierWidth,
                height: barrierHeight,
                _barrierColor));
    }

    private static Direction ChangeDirection()
    {
        if (!KeyAvailable)
            return Direction.Down;

        ConsoleKeyInfo key = Console.ReadKey(true);

        return _ = key.Key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            _ => Direction.Down, 
        };
    }

    private static void PlaySongGameOver()
    {
        Beep(300, 400); Beep(300, 400);
    }

    private static void PlaySongCheckPoint()
    {
        Beep(600, 300);
    }

    private void Update()
    {
        while (true)
        {
            Thread.Sleep(_gameSpeed);

            // draw score panel
            for (int i = 0; i < _barriers.Count; i++)
            {
                if (_barriers[i].Coordinates[_barriers[i].Width / 2, 0] == _bird.Head.X)
                {
                    _score++;

                    _playSongCheckPoint.Start();

                    _playSongCheckPoint = new Thread(PlaySongCheckPoint);

                    break;
                }
            }

            SetCursorPosition(0, _backgroundHeight);
            ForegroundColor = ConsoleColor.White;

            Write("score: " + _score.ToString());
            //

            // spawn barriers
            if (_barriers[^1].Coordinates[MaxBarrierWidth, 0] == WindowWidth - MaxBarrierWidth * 3)
                SpawnBarriers();
            //

            // game over
            void Lose()
            {
                ForegroundColor = ConsoleColor.DarkRed;

                _playSongGameOver.Start();

                _playSongGameOver = new Thread(PlaySongGameOver);

                int item = MenuBar.Menu(
                    new string[] { "Restart", "My Records", "Exit" },
                    new string[] { "You lose!" },
                    x: WindowWidth / 2, y: WindowHeight / 2,
                    arrow: ">");

                if (item == 3)
                    Environment.Exit(0);

                _records.Add(_score);

                if (item == 2)
                {
                    Clear();

                    SetCursorPosition(WindowWidth / 2, WindowTop);

                    WriteLine("My Highscores:");

                    // sort array
                    int[] newArray = new int[_records.Count];

                    for (int i = 0; i < _records.Count; i++)
                    {
                        for (int j = i + 1; j < _records.Count; j++)
                            if (_records[i] == _records[j])
                                _records = _records.Where((value, index) => index != j).ToList();
                    }

                    _records.CopyTo(newArray, 0);

                    for (int i = 0; i < _records.Count; i++)
                    {
                        int maxValue = newArray.Max();

                        _records[i] = maxValue;

                        newArray = newArray.Where(value => value != maxValue).ToArray();
                    }
                    //

                    for (int i = 0, padding = 3; i < _records.Count; i++, padding += 3)
                    {

                        if (i == 9)
                            break;

                        SetCursorPosition(WindowWidth / 2, padding);

                        WriteLine((i + 1) + ". " + _records[i]);

                        SetCursorPosition(0, padding + 1);

                        for (int j = 0; j < WindowWidth; j++)
                            Write("-");
                    }

                    ReadKey();

                    Clear();
                }

                _score = 0;

                _barriers = new List<Barrier>();

                _bird = new Bird(WindowWidth / 2, WindowHeight / 2, color: ConsoleColor.Magenta, birdSize: _birdSize);

                Start();
            }

            if (_bird.Head.Y <= WindowTop + 1 || _bird.Head.Y >= (WindowHeight - _backgroundHeight) - 2)
                Lose();

            for (int i = 0; i < _barriers.Count; i++)
            {
                for (int j = 0; j < _barriers[i].Width * _barriers[i].Height; j++)
                {
                    if (
                        _bird.Head.X == _barriers[i].Coordinates[j, 0]
                        &&
                            (
                            _bird.Head.Y == _barriers[i].Coordinates[j, 1] ||
                            _bird.Head.Y + 2 /* from the head +2 */ == _barriers[i].Coordinates[j, 1] ||
                            _bird.Head.Y - 1 == _barriers[i].Coordinates[j, 1]
                            )
                        )
                    {
                        Lose();
                    }
                }
            }
            //

            // move
            Direction direction = ChangeDirection();

            _bird.Move(direction);

            MoveBarriersForward();
            //

            // hide last barriers outside bounds
            if (_barriers[0].Coordinates[0, 0] <= WindowLeft + 1)
            {
                void RemoveBarrierColumn(int index)
                {
                    for (int i = 0, j = 0; i < _barriers[index].Height; i++, j += _barriers[index].Width - 1)
                    {
                        _barriers[index].Coordinates = RemoveRowFrom2DArray(
                                array: _barriers[index].Coordinates,
                                rowsQty: _barriers[index].Width * _barriers[index].Height - i,
                                columnsQty: 2,
                                index: j);
                    }

                    _barriers[index].Width--;
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0, k = 0; j < _barriers[i].Height; j++, k += _barriers[i].Width)
                    {
                        SetCursorPosition(_barriers[i].Coordinates[k, 0], _barriers[i].Coordinates[k, 1]);

                        Write(" ");
                    }

                    RemoveBarrierColumn(i);

                    if (_barriers[i].Coordinates.Length <= 0)
                        _barriers.RemoveAt(i);
                }
            }

        }
    }

    public void Start()
    {
        SpawnBarriers();

        DrawMap();

        Update();
    }
}
