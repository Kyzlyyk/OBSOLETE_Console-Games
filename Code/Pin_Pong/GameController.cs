namespace Pin_Pong;

using Draw;

class GameController
{
    public GameController(bool isMultiPlayer)
    {
        this.isMultiPlayer = isMultiPlayer;
    }

    private int _gameSpeed = 3;

    private static Pixel _ball =
        new(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.White, pixel: '■');

    public static readonly Player _leftPlayer =
        new(7, Console.WindowHeight / 2, TypeControl.WASD, ConsoleColor.Red);

    public static readonly Player _rightPlayer =
        new(Console.WindowWidth - 6, Console.WindowHeight / 2, TypeControl.NumPad, ConsoleColor.Blue);

    private int _leftPlayerPoint;
    private int _rightPlayerPoint;

    private bool isTop;
    private bool isLeft;

    private int borderLeft = 3;
    private int borderRight = Console.WindowWidth - 2;

    private bool isMultiPlayer;

    private void PlayerThreadMove()
    {

    }

    private void CheckBallBounce()
    {

        for (int i = 0; i < _rightPlayer.PlayerParthes.Length; i++)
        {
            if (
                _ball.X == _rightPlayer.PlayerParthes[i].X - 1
                &&
                _ball.Y == _rightPlayer.PlayerParthes[i].Y
                || _ball.X == borderRight - 1
                || _ball.X == _leftPlayer.PlayerParthes[i].X - 1
                &&
                _ball.Y == _leftPlayer.PlayerParthes[i].Y
                )
            {
                isLeft = false;
            }
        }

        for (int i = 0; i < _leftPlayer.PlayerParthes.Length; i++)
        {
            if (
                _ball.X == _leftPlayer.PlayerParthes[i].X + 1
                &&
                _ball.Y == _leftPlayer.PlayerParthes[i].Y
                || _ball.X <= borderLeft + 1
                || _ball.X == _rightPlayer.PlayerParthes[i].X + 1
                &&
                _ball.Y == _rightPlayer.PlayerParthes[i].Y
                )
            {
                isLeft = true;
            }
        }

        if (_ball.Y >= Console.WindowHeight - 2)
            isTop = false;

        if (_ball.Y <= 2)
            isTop = true;

        // top left
        if (isTop && isLeft)
        {
            _ball.X++;
            _ball.Y++;
        }

        // top right
        if (isTop && !isLeft)
        {
            _ball.X--;
            _ball.Y++;
        }

        // bottom left
        if (!isTop && isLeft)
        {
            _ball.X++;
            _ball.Y--;
        }

        // bottom right
        if (!isTop && !isLeft)
        {
            _ball.X--;
            _ball.Y--;
        }
    }

    private void Update()
    {
        while (true)
        {
            // check points

            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(20, 2);
            Console.Write("Red: " + _leftPlayerPoint);

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(Console.WindowWidth - 20, 2);
            Console.Write("Blue: " + _rightPlayerPoint);

            if (_ball.X >= borderRight - 1)
                _leftPlayerPoint++;

            if (_ball.X <= borderLeft + 1)
                _rightPlayerPoint++;
            //


            // players

           
            if (!isMultiPlayer)
                _leftPlayer.AIMove(ball: _ball, Direction.Left, rangeVision: 35);

            if (_ball.X < Console.WindowWidth / 2 && isMultiPlayer)
            {
                _leftPlayer.Move();
            } else 
                _rightPlayer.Move();
            // 


            // ball set

            CheckBallBounce();

            _ball.Draw();

            Thread.Sleep(_gameSpeed * 10);

            _ball.Clear();
            // 
        }
    }

    public void Start()
    {
        _rightPlayer.Spawn();
        _leftPlayer.Spawn();

        Update();
    }
}

