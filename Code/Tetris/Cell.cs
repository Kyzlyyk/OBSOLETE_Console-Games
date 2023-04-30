using Draw;

namespace Tetris;

internal struct Cell
{
	public Cell(int x, int y, double radius, ConsoleColor color = ConsoleColor.White)
	{
		X = x;
		Y = y;

		_radius = radius;

		_color = color;

		IsMarked = false;
	}

	public int X { get; set; }
	public int Y { get; set; }

	public bool IsMarked { get; set; }

	private readonly double _radius;

	private readonly ConsoleColor _color;


	public void Draw()
	{
		new Figure(X, Y, radiusX: _radius, radiusY: _radius, _color)
			.DrawSquare(TypeOfSquare.Full);
    }

    public void Clear()
	{
        new Figure(X, Y, radiusX: _radius, radiusY: _radius, ConsoleColor.Black)
			.DrawSquare(TypeOfSquare.Full);
    }
}