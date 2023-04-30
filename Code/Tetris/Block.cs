namespace Tetris;

internal class Block
{
	public Block(int initialX, int initialY, int radius, TypeOfBlock typeOfBlock, ConsoleColor color = ConsoleColor.Red, byte blockSize = 4)
	{
		X = initialX;
		Y = initialY;

		_radius = radius;

		_color = color;

		Cells = new Cell[blockSize];

		for (int i = 0; i < Cells.Length; i++)
		{
			Cells[i] = new Cell(initialX, initialY, radius, color: color);

			if (typeOfBlock == TypeOfBlock.Horizontal)
			{
                Cells[i].X = initialX += radius * 4;
                Cells[i].Y = initialY;
            }

			if (typeOfBlock == TypeOfBlock.Vertical)
			{
                Cells[i].X = initialX;
                Cells[i].Y = initialY += radius * 2;
            }

			if (typeOfBlock == TypeOfBlock.Zet)
			{
				if (i <= 1)
					Cells[i].X = initialX += radius * 4;
				if (i == 2)
					Cells[i].Y = initialY += radius * 2;
				if (i >= 3)
                    Cells[i].X = initialX += radius * 4;
            }

			if (typeOfBlock == TypeOfBlock.Full)
			{
                if (i <= 1)
                    Cells[i].X = initialX += radius * 4;

                if (i == 2)
				{
                    Cells[i].Y = initialY += radius * 2;

					Cells[i].X = initialX -= radius * 4;
                }

                if (i >= 3)
                    Cells[i].X = initialX += radius * 4;
            }

			if (typeOfBlock == TypeOfBlock.Pistol)
			{
                if (i <= 2)
                    Cells[i].X = initialX += radius * 4;
                if (i == 3)
                    Cells[i].Y = initialY += radius * 2;
                if (i >= 4)
                    Cells[i].X = initialX += radius * 4;
            }

			if (typeOfBlock == TypeOfBlock.Hummer)
			{
                if (i == 0)
                    Cells[i].X = initialX += radius * 4;
                if (i == 1)
				{
                    Cells[i].Y = initialY += radius * 2;

                    Cells[i].X = initialX -= radius * 4;
                }
                if (i >= 2)
                    Cells[i].X = initialX += radius * 4;
            }

            Cells[i].Draw();
		}
	}

	public int X { get; set; }
	public int Y { get; set; }

	public Cell[] Cells { get; set; }

	private int _radius;

	private ConsoleColor _color;

    public void Move(Direction direction = Direction.Down)
	{
        for (int i = 0; i < Cells.Length; i++)
        {
			Clear();

            switch (direction)
            {
				case Direction.Down: Cells[i].Y += _radius * 2; break;
                
				case Direction.Left: Cells[i].X -= _radius * 4; Cells[i].Y += _radius * 2; break;
                
				case Direction.Right: Cells[i].X += _radius * 4; Cells[i].Y += _radius * 2; break;
            }

            Draw();
        }
    }

	public void Draw()
	{
		Console.ForegroundColor = _color;

		for (int i = 0; i < Cells.Length; i++)
		{
            Cells[i].Draw();
		}

	}

	public void Clear()
	{
		for (int i = 0; i < Cells.Length; i++)
		{
			Cells[i].Clear();
        }
    }
}

