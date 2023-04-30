using static System.Console;

namespace FlappyBird;

internal sealed class Barrier
{
    public Barrier(int initialX, int initialY, int width, int height, ConsoleColor color = ConsoleColor.White)
    {
        Height = height;
        Width = width;

        Color = color;

        int primaryX = initialX;

        Coordinates = new byte[(width * height) * 2, 2];

        for (byte y = 0, i = 0; y < height; y++)
        {
            for (byte x = 0; x < width; x++, i++)
            {
                Coordinates[i, 0] = (byte)initialX;
                Coordinates[i, 1] = (byte)initialY;

                initialX++;
            }

            initialY++;
            initialX = primaryX;
        }
    }

    public byte[,] Coordinates { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public ConsoleColor Color { get; set; }

    public void Clear()
    {
        for (int i = 0; i < Width * Height; i++)
        {
            SetCursorPosition(Coordinates[i, 0], Coordinates[i, 1]);

            Write(" ");
        }
    }
}
