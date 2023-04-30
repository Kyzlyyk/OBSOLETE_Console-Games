namespace Mario;

internal sealed class Enemy : Entity
{
    public Enemy(int x, int y, int width = 1, int height = 1, int colliderWidth = 0, int colliderHeight = 0, bool colliderIsOn = true)
    {
        ColliderIsOn = colliderIsOn;

        ColliderCenterX = x + width;
        ColliderCenterY = y + height;

        ColliderWidth = colliderWidth;
        ColliderHeight = colliderHeight;

        X = x + width;
        Y = y + height;
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public override bool ColliderIsOn { get; set; }
    
    public override int ColliderCenterX { get; set; }
    public override int ColliderCenterY { get; set; }

    public override int ColliderWidth { get; set; }
    public override int ColliderHeight { get; set; }

    public override void Draw()
    {
        Console.SetCursorPosition(X, Y);

        Console.ForegroundColor = ConsoleColor.Red;

        Console.Write("█");
    }

    public override void Clear()
    {
        Console.SetCursorPosition(X, Y);

        Console.Write(" ");
    }
}
