using ToolBar;
using static System.Console;

namespace Mario;

internal sealed class GameController
{
    private Player _player = new(0, 0);

    private List<Entity> _enemies = new();

    private void DrawMap()
    {
        ForegroundColor = ConsoleColor.DarkGreen;

        for (int i = 0; i < WindowWidth; i++)
        {
            SetCursorPosition(i, WindowHeight / 2 + 2);

            Write("█");
        }
    }

    private Direction ChangeDirection()
    {
        ConsoleKeyInfo key = ReadKey(true);

        return _ = key.Key switch
        {
            ConsoleKey.RightArrow => Direction.Right,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.Spacebar => Direction.Up,
            _ => Direction.Down,
        };
    }

    private void Update()
    {
        while (true)
        {
            bool isEncountered = Collider.CheckColliders(_player, _enemies);

            if (isEncountered)
            {
                //_player.Jump();

                while (_player.ColliderCenterY - _player.Height <= WindowHeight - 3)
                {
                    _player.Move(Direction.Down);

                    Thread.Sleep(30);
                }

                _player.Clear();

                SetCursorPosition(WindowWidth / 2, WindowHeight / 2);

                int item = MenuBar.Menu(
                    new string[] { "Restart", "Exit" },
                    new string[] { "You lose!" },
                    WindowWidth / 2, WindowHeight / 2 - 5,
                    ">");

                if (item == 2) 
                    Environment.Exit(0);

                Start();
            }

            _enemies[0].Draw();

            if (!KeyAvailable)
                continue;

            Direction direction = ChangeDirection();
        
            _player.Move(direction);
        }
    }

    public void Start()
    {
        _enemies.Add(new Enemy(WindowWidth / 2, WindowHeight / 2));

        _player = new(0, WindowHeight / 2);

        DrawMap();

        Update();
    }
}
