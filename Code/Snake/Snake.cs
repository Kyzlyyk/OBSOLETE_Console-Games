using Draw;
using System.Drawing;

namespace Snake;

internal class Snake
{
	private ConsoleColor _headColor;
    private ConsoleColor _bodyColor;

    public Snake(int initialX, int initialY, ConsoleColor bodyColor, ConsoleColor headColor, int bodyLength = 3)
	{
		_headColor = headColor;
		_bodyColor = bodyColor;

		Head = new Pixel(initialX, initialY, headColor);

		for (int i = bodyLength; i >= 0; i--)
		{
			Body.Enqueue(new Pixel(initialX - i - 1, initialY, bodyColor, '■'));
		}
	}

	public Pixel Head { get; private set; }
	public Queue<Pixel> Body { get; } = new Queue<Pixel>();

    public void Move(Direction direction)
    {
		Clear();

		Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor, '■'));

		Body.Dequeue();

		Head = direction switch
		{
			Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor, '■'),
            Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor, '■'),
            Direction.Up => new Pixel(Head.X, Head.Y - 1, _headColor),
            Direction.Down => new Pixel(Head.X, Head.Y + 1, _headColor),
			_ => Head
        };

		Draw();
    }

    public void Draw()
	{
		Head.Draw();

		foreach (Pixel pixel in Body)
		{
			pixel.Draw();
		}
	}

	public void Clear()
	{
		Head.Clear();

		foreach (Pixel pixel in Body)
		{
			pixel.Clear();
		}
	}
}
