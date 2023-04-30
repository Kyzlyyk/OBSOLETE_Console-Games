namespace AIDraw;

public class Picture
{
    public Picture(int x, int y)
    {
        _x = x;
        _y = y;

        _currentColor = ConsoleColor.Red;
    }

    private int _x;
    private int _y;

    private ConsoleColor _currentColor;

    private static int[,] AddRowTo2DArray(int[,] array, int rowsQty, int columnsQty, int[] values)
    {
        if (values.Length < columnsQty)
            throw new ArgumentException("int[] values length must not be less or higher int columnsQty");

        int[,] newArray = new int[rowsQty + 1, columnsQty];

        newArray[rowsQty, 0] = values[0];
        newArray[rowsQty, 1] = values[1];

        for (int i = 0; i < rowsQty; i++)
        {
            for (int j = 0; j < columnsQty; j++)
            {
                newArray[i, j] = array[i, j];
            }
        }

        return newArray;
    }


    private void Draw(int x, int y)
    {
        Console.SetCursorPosition(x, y);

        Console.Write("█");
    }

    public (int[,], int[] center) GetPicture()
    {
        Console.SetCursorPosition(_x, _y);
        Console.CursorVisible = true;

        int rowsQty = 0;
        int[,] coordinates = new int[rowsQty, 2];

        int[] center = new int[2] { _x, _y };

        Draw(center[0], center[1]);

        coordinates = AddRowTo2DArray(coordinates, rowsQty, 2, new int[] { _x, _y });
        rowsQty++;

        while (true)
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Current color: ");

            Console.ForegroundColor = _currentColor;
            Console.Write("█");

            Console.CursorVisible = true;

            ConsoleKeyInfo key = Console.ReadKey(true);

            while (key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow: if (_x != Console.WindowWidth - 3) _x++; break;
                    case ConsoleKey.LeftArrow: if (_x != Console.WindowLeft) _x--; break;
                    case ConsoleKey.UpArrow: if (_y != Console.WindowTop) _y--; break;
                    case ConsoleKey.DownArrow: if (_y != Console.WindowHeight - 3) _y++; break;

                    case ConsoleKey.R: Console.ForegroundColor = _currentColor = ConsoleColor.Red; break;
                    case ConsoleKey.B: Console.ForegroundColor = _currentColor = ConsoleColor.Blue; break;
                    case ConsoleKey.G: Console.ForegroundColor = _currentColor = ConsoleColor.Green; break;
                    case ConsoleKey.Y: Console.ForegroundColor = _currentColor = ConsoleColor.Yellow; break;
                    case ConsoleKey.W: Console.ForegroundColor = _currentColor = ConsoleColor.White; break;

                    case ConsoleKey.Tab:
                        Console.CursorVisible = false;
                        Console.Clear();

                        return (coordinates, center);
                }

                Console.SetCursorPosition(_x, _y);
            }

            Console.ForegroundColor = _currentColor;

            Draw(_x, _y);

            coordinates = AddRowTo2DArray(coordinates, rowsQty, 2, new int[] { _x, _y });

            rowsQty++;
        }
    }
}