using static System.Console;
using Draw;

namespace FlappyBird;

internal sealed class Bird
{
    public Bird(int initialX, int initialY, char pixel = '■', ConsoleColor color = ConsoleColor.White, byte birdSize = 3)
    {
        _x = initialX;
        _y = initialY;

        _birdSize = birdSize;

        _color = color;
        _pixel = pixel;

        Draw();
    }

    private List<Pixel> _pixels = new(1);

    public Pixel Head { get; private set; } = new();

    private int _x;
    private int _y;

    private readonly byte _birdSize;

    private readonly ConsoleColor _color;
    private readonly char _pixel;

    public void Move(Direction direction)
    {
        Clear();

        switch (direction)
        {
            case Direction.Up:
                _y -= 2;
                break;
            case Direction.Down:
                _y++;
                break;
        }

        Draw();
    }

    private void Draw()
    {
        ForegroundColor = _color;

        for (int i = 0; i < _birdSize; i++)
        {
            _pixels.Add(new Pixel(_x++, _y, _color));
        }

        _x -= _birdSize - 1;
        _y--;

        for (int i = 0; i < _birdSize; i++)
        {
            _pixels.Add(new Pixel(_x++, _y, _color));
        }

        for (int i = 0; i < _pixels.Count; i++)
        {
            _pixels[i].Draw();
        }

        Head = new Pixel(_x, _y, color: ConsoleColor.Yellow);

        Head.Draw();

        _y++;
        _x -= _birdSize + 1;
    }

    private void Clear()
    {
        for (int i = _pixels.Count - 1; i >= 0; i--)
        {
            _pixels[i].Clear();

            _pixels.RemoveAt(i);
        }

        Head.Clear();
    }
}
