namespace Snake;

using Draw;
using ToolBar;

class GameController
{
    public GameController(int gameSpeed)
    {
        _gameSpeed = gameSpeed * 10;
    }

    private readonly int _gameSpeed;

    private Direction _currentDirection = Direction.Right;

    private const ConsoleColor _headColor = ConsoleColor.DarkGray;
    private const ConsoleColor _bodyColor = ConsoleColor.White;

    private Snake _snake = 
        new(Console.WindowWidth / 2, Console.WindowHeight / 2,
            headColor: _headColor, bodyColor: _bodyColor, bodyLength: 3);

    private Pixel _apple = new(0, 0);

    private readonly int _borderLeft = 6;
    private readonly int _borderRight = Console.WindowWidth - 6;

    private readonly int _borderTop = 4;
    private readonly int _borderBottom = Console.WindowHeight - 4;

    private int _count = 0;

    private Direction ChangeDirection(Direction currentDirection)
    {
        if (!Console.KeyAvailable)
            return currentDirection;


        ConsoleKeyInfo key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow: 
               if (_currentDirection != Direction.Down) 
                    return Direction.Up;
                break;

            case ConsoleKey.DownArrow: 
                if (_currentDirection != Direction.Up)
                    return Direction.Down;
                break;

            case ConsoleKey.LeftArrow: 
                if (_currentDirection != Direction.Right)
                    return Direction.Left;
                break;

            case ConsoleKey.RightArrow: 
                if (_currentDirection != Direction.Left)
                    return Direction.Right;
                break;
        }

        return currentDirection;
    }

    private void Update()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(_borderLeft + 4, _borderTop + 2);

            Console.Write("Points: " + _count);

            void Lose()
            {
                Console.Clear();

                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write("GAME OVER!");

                Console.ForegroundColor = ConsoleColor.Gray;

                int item = MenuBar.Menu(new string[] { "Restart", "Exit" }, x: Console.WindowWidth / 2, y: Console.WindowHeight / 2 + 2, arrow: ">");

                if (item == 1)
                {
                    // draw border
                    new Figure(Console.WindowWidth / 2, Console.WindowHeight, 22, ConsoleColor.White).DrawSquare();

                    _count = 0;

                    // reset game
                    Start();
                }

                if (item == 2)
                    return;
            }

            // apple update
            if (_snake.Head.X == _apple.X && _snake.Head.Y == _apple.Y)
            {
                _count++;

                // if the head touched the body
                foreach (Pixel pixel in _snake.Body)
                {
                    if (_snake.Head.Y == pixel.Y && _snake.Head.X == pixel.X)
                    {
                        Lose();
                        return;
                    }
                }
                // if the head touched the body

                _apple.Clear();


                for (int i = _snake.Body.Count; i >= 0; i--)
                {
                    if (i == 0)
                        _snake.Body.Enqueue(new Pixel(_snake.Head.X - i - 1, _snake.Head.Y, _bodyColor));
                }

                _apple =
                new
                (
                    new Random().Next(_borderLeft + 1, _borderRight - 1),
                    new Random().Next(_borderTop + 1, _borderBottom - 1),
                    ConsoleColor.Red, pixel: '■'
                );

                _apple.Draw();
            }
            // apple update

            
            // change snake direction
            Direction direction = ChangeDirection(_currentDirection);

            Thread.Sleep(_gameSpeed);

            _snake.Move(direction);

            _currentDirection = direction;

            // game over
            if (
                _snake.Head.X == _borderRight + 1
                || _snake.Head.X == _borderLeft - 1
                || _snake.Head.Y == _borderTop
                || _snake.Head.Y == _borderBottom
                )
            {
                Lose();
                return;
            }
            //game over

        }
    }

    public void Start()
    {
        // spawn snake
        _snake =
        new(Console.WindowWidth / 2, Console.WindowHeight / 2,
            headColor: _headColor, bodyColor: _bodyColor, bodyLength: 3);

        // reset default direction
        _currentDirection = Direction.Right;

        // spawn food (apple)
        _apple = 
            new (
                new Random().Next(_borderLeft + 1, _borderRight - 1),
                new Random().Next(_borderTop + 1, _borderBottom - 1), 
                ConsoleColor.Red, '■'
            );
       
        _apple.Draw();

        // start loop
        Update();
    }
}
