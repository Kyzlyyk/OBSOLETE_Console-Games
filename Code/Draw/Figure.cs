namespace Draw;

public class Figure
{
    public Figure(int x, int y, double radius, ConsoleColor color = ConsoleColor.White, char pixel = '▀')
    {
        _radius = radius;

        _pixel = pixel;

        _color = color;

        X = x;
        Y = y;
    }

    private readonly double _radius;

    private readonly char _pixel;

    private readonly ConsoleColor _color;

    public int X { get; private set; }
    public int Y { get; private set; }

    public void DrawSquare()
    {
        int _borderTop = (int)(Y - _radius) / 2;
        int _borderBottom = (int)(Y + _radius) / 2;

        int _borderLeft = (int)(X / 2  - _radius) * 2;
        int _borderRight = (int)(X / 2 + _radius) * 2;

        // left
        for (int i = _borderTop; i < _borderBottom; i++)
        {
            new Pixel(_borderLeft, i, color: _color).Draw();
        }

        // bottom
        for (int i = _borderLeft; i < _borderRight + 1; i++)
        {
            new Pixel(i, _borderBottom, pixel: '▀', color: _color).Draw();
        }

        // top
        for (int i = _borderLeft + 1; i < _borderRight; i++)
        {
            new Pixel(i, _borderTop, pixel: '▀', color: _color).Draw();
        }

        // right
        for (int i = _borderTop; i < _borderBottom; i++)
        {
            new Pixel(_borderRight, i, color: _color).Draw();
        }
    }

    public void DrawCircle()
    {
        Console.ForegroundColor = _color;

        int x = X, y = (int)(Y - _radius);

        void Draw()
        {
            Console.SetCursorPosition(x * 2, y);

            Console.Write(_pixel);
        }


        for (int i = 0; i < _radius; i++, x--, y++)
            Draw();

        for (int i = 0; i < _radius; i++, x++, y++)
            Draw();

        for (int i = 0; i < _radius; i++, x++, y--)
            Draw();

        for (int i = 0; i < _radius; i++, x--, y--)
            Draw();
    }
}
