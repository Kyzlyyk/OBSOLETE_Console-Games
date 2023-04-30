using Newtonsoft.Json;

namespace Mario;

internal sealed class Player : Entity
{
    public Player(
        int x, int y,
        int colliderWidth = 9, int colliderHeight = 20,
        bool colliderOn = true)
    {
        ColliderWidth = colliderWidth;
        ColliderHeight = colliderHeight;

        FileStream txt = File.OpenRead(@"C:\Users\Vlad\Desktop\C#\MyProjects\MyProjects\AIDraw\Coordinates\Coordinates.txt");

        _coordinates = new int[,]
        {
            {30, 12},
            {30,12},
            {30,10},
            {31,9},
            {32,9},
            {33,9},
            {30,8},
            {29,8},
            {28,8},
            {27,8},
            {26,9},
            {29,13},
            {28,14},
            {27,14},
            {26,14},
            {27,13},
            {28,13},
            {31,13},
            {32,14},
            {33,14},
            {32,13},
            {34,14},
            {33,13},
            {31,12},
            {32,12},
            {29,12},
            {28,12},
            {29,11},
            {31,11},
            {32,11},
            {33,11},
            {28,11},
            {27,11},
            {26,11},
            {34,11},
            {29,10},
            {31,10},
            {27,10},
            {27,10},
            {28,10},
            {27,9},
            {29,9},
            {28,9},
            {26,8},
            {27,7},
            {28,7},
            {29,7},
            {30,7},
            {31,8}
        };

        ColliderCenterX = 30;
        ColliderCenterY = 12;
        
        Draw();
    }

    public override bool ColliderIsOn { get; set; } = true;
    
    public override int ColliderCenterX { get; set; }
    public override int ColliderCenterY { get; set; }

    public override int ColliderWidth { get; set; }
    public override int ColliderHeight { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    private int[,] _coordinates;

    public void Jump()
    {
        for (int i = 1; i <= 2; i++)
        {
            Clear();

            for (int j = 0; j < _coordinates.Length / 2; j++)
                _coordinates[i, 1]--;

            Thread.Sleep(10);

            Draw();

            Thread.Sleep(10);
        }


        for (int i = 1; i <= 2; i++)
        {
            Clear();

            for (int j = 0; j < _coordinates.Length / 2; j++)
                _coordinates[j, 1]++;

            Thread.Sleep(100);

            Draw();
        }
    }

    public void Move(Direction direction)
    {
        Clear();

        for (int i = 0; i < _coordinates.Length / 2; i++)
        {
            switch (direction)
            {
                case Direction.Right: _coordinates[i, 0]++; break;
                case Direction.Left: _coordinates[i, 0]--; break;
                case Direction.Up: Jump(); break;
                case Direction.Down: _coordinates[i, 1]++; break;
                default: break;
            };
        }

        if (direction == Direction.Right)
            ColliderCenterX++;
        
        if (direction == Direction.Left)
            ColliderCenterX--;
        
        if (direction == Direction.Down)
            ColliderCenterY++;

        Draw();
    }

    public override void Draw()
    {
        for (int i = 0; i < _coordinates.Length / 2; i++)
        {
            Console.SetCursorPosition(_coordinates[i, 0], _coordinates[i, 1]);

            Console.Write("█");
        }
    }
    
    public override void Clear()
    {
        for (int i = 0; i < _coordinates.Length / 2; i++)
        {
            Console.SetCursorPosition(_coordinates[i, 0], _coordinates[i, 1]);

            Console.Write(" ");
        }
    }
}
