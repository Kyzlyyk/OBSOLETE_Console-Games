namespace Draw;

public struct Pixel
{
    public Pixel(int x, int y, ConsoleColor color = ConsoleColor.White, char pixel = '█')
    {
        X = x;
        Y = y;

        Color = color;
        _pixel = pixel;
    }

    private char _pixel = '█'; /* ▀ ; ■ */

    public ConsoleColor Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public void Draw()
    {
        Console.ForegroundColor = Color;
     
        Console.SetCursorPosition(X, Y);
        Console.Write(_pixel);
    }

    public void Clear()
    {
        Console.SetCursorPosition(X, Y);
        Console.Write(" ");
    }
}
