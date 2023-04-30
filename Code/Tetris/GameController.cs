using Draw;
using ToolBar;

namespace Tetris;

internal class GameController
{
    public GameController(int gameSpeed = 1)
    {
        _gameSpeed = gameSpeed;
    }

    private int _gameSpeed;

    private const byte _blockSize = 4;

    private const int _rowsQty = 18;
    private const int _cellsQty = 16;

    private const int _borderTop = 5;
    private readonly int _borderBottom = Console.WindowHeight - 10;

    private const int _borderLeft = 11;
    private readonly int _borderRight = Console.WindowWidth - 10;

    private const int _radiusOfCell = 1;

    private bool _isPlayerChanged = false;

    private int _currentScore = 0;
    private int _highScore = 0;

    private Cell[,] _rowCell = new Cell[_rowsQty, _cellsQty];

    private readonly TypeOfBlock[] _typeOfBlocks = new TypeOfBlock[] 
    {
        TypeOfBlock.Full, 
        TypeOfBlock.Horizontal, 
        TypeOfBlock.Vertical,
        TypeOfBlock.Pistol,
        TypeOfBlock.Zet,
    };

    private readonly ConsoleColor[] _consoleColors = new ConsoleColor[]
    {
        ConsoleColor.Red,
        ConsoleColor.Blue,
        ConsoleColor.Cyan,
        ConsoleColor.Yellow,
        ConsoleColor.Green,
    };

    private readonly List<Block> _blocks = new List<Block>(20);

    private Block _currentBlock = 
        new (Console.WindowWidth / 2 - (_cellsQty / 2), _borderTop, _radiusOfCell, TypeOfBlock.Full, blockSize: _blockSize);


    private void CellInitialization()
    {
        int x = _borderLeft, y = _borderTop;

        for (int i = 0; i < _rowsQty; i++)
        {
            for (int j = 0; j < _cellsQty; j++)
            {
                _rowCell[i, j] = new Cell(x += _radiusOfCell * 4, y, _radiusOfCell)
                {
                    IsMarked = false
                };

                //_rowCell[i, j].Draw();
            }

            x = _borderLeft;
            y += _radiusOfCell * 2;
        }
    }

    private void DrawPanel()
    {
        new Figure((_borderRight + _borderLeft) / 2, _borderBottom + _borderTop, 17, 3).DrawSquare();

        new Figure((_borderRight + _borderLeft) / 2, _borderBottom + _borderTop, 8, 3).DrawSquare();

        Console.SetCursorPosition((_borderLeft + _borderRight) / 2, _borderBottom + _borderTop);
        Console.Write("Score: " + _currentScore.ToString());

        Console.SetCursorPosition((_borderLeft + _borderRight) / 2, _borderBottom + _borderTop + _borderTop / 2);
        Console.Write("HighScore: " + _highScore);
    }

    private void ChangePlayer()
    {
        _isPlayerChanged = true;

        for (int i = _rowsQty - 1; i >= 0; i--)
        {
            for (int j = 0; j < _cellsQty; j++)
            {
                for (int k = 0; k < _currentBlock.Cells.Length; k++)
                {
                    if (_rowCell[i, j].X == _currentBlock.Cells[k].X 
                        && _rowCell[i, j].Y == _currentBlock.Cells[k].Y)
                    {
                        _rowCell[i, j].IsMarked = true;
                    }
                }
            }
        }

        _blocks.Add(_currentBlock);

        _currentBlock =
                new Block(Console.WindowWidth / 2 - (_cellsQty / 2), _borderTop,
                radius: _radiusOfCell,
                color: _consoleColors[new Random().Next(_consoleColors.Length)],
                typeOfBlock: _typeOfBlocks[new Random().Next(_typeOfBlocks.Length)], 
                blockSize: _blockSize);
    }

    private Direction ChangeDirection()
    {
        bool keyAvailable = false;

        if (!Console.KeyAvailable)
        {
            if (_currentBlock.Cells[^1].Y >= _borderBottom - (_radiusOfCell * 2))
            {
                ChangePlayer();
            }

            keyAvailable = true;
        }

        if (keyAvailable)
        {
            return Direction.Down;
        }

        ConsoleKeyInfo key = Console.ReadKey(true);

        if (_currentBlock.Cells[^1].Y >= _borderBottom - (_radiusOfCell * 2))
        {
            ChangePlayer();

            return Direction.Down;
        }

        switch (key.Key)
        {
            case ConsoleKey.DownArrow:
                if (_currentBlock.Cells[^1].Y < _borderBottom - (_radiusOfCell * 2))
                    return Direction.Down;
                break;

            case ConsoleKey.RightArrow:
                if (_currentBlock.Cells[^1].X < _borderRight - (_radiusOfCell * 4))
                    return Direction.Right;
                break;

            case ConsoleKey.LeftArrow:
                if (_currentBlock.Cells[0].X > _borderLeft + (_radiusOfCell * 4))
                    return Direction.Left;
                break;
        }

        return Direction.Down;
    }

    private void Update()
    {
        while (true)
        {
            Direction direction = Direction.Down;

            direction = ChangeDirection();

            // check collision
            for (int i = 0; i < _blocks.Count; i++) // all blocks on the map
            {
                for (int j = 0; j < _blocks[i].Cells.Length; j++) // all cells in the blocks
                {
                    for (int k = 0; k < _currentBlock.Cells.Length; k++) // all cells in the player block
                    {
                        // go through all possible cases of placing cells // _blocks[i].Cells[0].X, _blocks[i].Cells[1].X ...
                        for (int p = 0; p < _blocks[i].Cells.Length; p++)
                        {
                            // check collision upper - left/ upper - right
                            if ((_currentBlock.Cells[k].X == _blocks[i].Cells[j].X + (_radiusOfCell * 4)
                                || _currentBlock.Cells[k].X == _blocks[i].Cells[j].X - (_radiusOfCell * 4))
                                && _currentBlock.Cells[k].Y == _blocks[i].Cells[p].Y - (_radiusOfCell * 2))
                            {
                                direction = Direction.Down;
                                break;
                            }

                            // check collision only upper
                            if (_currentBlock.Cells[k].X == _blocks[i].Cells[p].X
                                && _currentBlock.Cells[k].Y == _blocks[i].Cells[p].Y - (_radiusOfCell * 2))
                            {
                                ChangePlayer();
                            }
                        }
                    }
                }
            }


            // game over?
            if (_blocks.Count > 0 && _blocks[^1].Cells[0].Y <= _borderTop)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.Write("GAME OVER!");

                Console.ForegroundColor = ConsoleColor.Gray;
                int item = MenuBar.Menu(new string[] { "Restart", "Exit" }, x: Console.WindowWidth / 2, y: Console.WindowHeight / 2 + 2, arrow: ">");

                if (item == 1)
                {
                    _highScore = _currentScore;

                    _currentScore = 0;

                    _currentBlock =
                        new(Console.WindowWidth / 2 - (_cellsQty / 2), _borderTop,
                        radius: _radiusOfCell,
                        typeOfBlock: _typeOfBlocks[new Random().Next(_typeOfBlocks.Length)],
                        color: _consoleColors[new Random().Next(_consoleColors.Length)],
                        blockSize: _blockSize);

                    _rowCell = new Cell[_rowsQty, _cellsQty];
                    
                    for (int i = _blocks.Count - 1; i >= 0; i--)
                    {
                        _blocks.RemoveAt(i);
                    }

                    Console.Clear();

                    Start();
                }
                
                return;
            }

            _isPlayerChanged = false;

            _currentBlock.Move(direction);

            Thread.Sleep(_gameSpeed);


            // check if the whole row is filled with blocks
            if (_isPlayerChanged)
                continue;

            for (int i = _rowsQty - 1; i >= 0; i--)
            {
                for (int j = 0; j < _cellsQty; j++)
                {
                    // if block in this cell
                    if (_rowCell[i, j].IsMarked && j == _cellsQty - 1)
                    {
                        // check this row cells
                        for (int k = 0; k < _cellsQty; k++)
                        {
                            for (int p = 0; p < _blocks.Count; p++)
                            {
                                for (int d = 0; d < _blocks[p].Cells.Length; d++)
                                {
                                    // check whether block cell in this row cell
                                    if (_rowCell[i, k].X == _blocks[p].Cells[d].X
                                        && _rowCell[i, k].Y == _blocks[p].Cells[d].Y)
                                    {
                                        // remove this block cell
                                        _blocks[p].Cells[d].Clear();

                                        _blocks[p].Cells = _blocks[p].Cells.Where((val, index) => index != d).ToArray();

                                        if (_blocks[p].Cells.Length == 0)
                                        {
                                            _blocks.RemoveAt(p);
                                            break;
                                        }

                                        // row cell is empty
                                        _rowCell[i, k].IsMarked = false;

                                        // up score
                                        DrawPanel();
                                        _currentScore += 1000;

                                        if (_currentScore >= 150000 && _currentScore < 160000)
                                            _gameSpeed -= 15;

                                        if (_currentScore >= 1050000 && _currentScore < 1060000)
                                            _gameSpeed -= 15;
                                    }
                                }
                            }
                        }
                    }
                    if (!_rowCell[i, j].IsMarked)
                        break;
                }
            }

        }
    }

    public void Start()
    {
        // draw border
        new Figure(Console.WindowWidth / 2 + 2, Console.WindowHeight / 2 - 3,
            radiusX: 17, radiusY: 19)
            .DrawSquare();

        _currentBlock =
            new(Console.WindowWidth / 2 - (_cellsQty / 2), _borderTop,
            radius: _radiusOfCell,
            typeOfBlock: _typeOfBlocks[new Random().Next(_typeOfBlocks.Length)],
            color: _consoleColors[new Random().Next(_consoleColors.Length)],
            blockSize: _blockSize);

        CellInitialization();

        DrawPanel();

        Update();
    }
}
