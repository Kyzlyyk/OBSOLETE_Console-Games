namespace Tic_Tac_Toe;

using Draw;

class Program
{

    static void Main()
    {
        Console.SetWindowSize(44, 23);
        Console.SetBufferSize(44, 23);

        Console.CursorVisible = false;

        int item = Menu(new string[] { "Play", "Exit" }, Console.WindowWidth / 2, Console.WindowHeight / 2);

        int scale = 3;

        if (item == 1)
        {
            int itemScale = Menu(new string[] { "3x3", "6x6", "8x8" }, 
                Console.WindowWidth / 2, Console.WindowHeight / 2);

            if (itemScale == 1)
            {
                scale = 3;

                Console.SetWindowSize(44, 23);
                Console.SetBufferSize(44, 23);
            }
            if (itemScale == 2)
            {
                scale = 6;

                Console.SetWindowSize(80, 40);
                Console.SetBufferSize(80, 40);
            }
            if (itemScale == 3)
            {
                scale = 8;

                Console.SetWindowSize(105, 52);
                Console.SetBufferSize(105, 52);
            }
        }

        if (item == 2)
        {
            return;
        } 

        new GameController(sizeOfMap: scale).Start();
    }

    static int Menu(string[] itemLabel, int x = 37, int y = 11, string arrow = "->")
    {
        int items = itemLabel.Length;

        int xItem = x + arrow.Length + 1;
        int yItem = y;

        // display the label for the item
        for (int i = 0; i < itemLabel.Length; i++)
        {
            Console.SetCursorPosition(xItem, yItem++);

            Console.Write(itemLabel[i]);
        }

        // first arrow
        Console.SetCursorPosition(x, y);
        Console.Write(arrow);

        void ClearArrow()
        {
            for (int i = 0; i < arrow.Length; i++)
            {
                Console.SetCursorPosition(x++, y);

                Console.Write(" ");
            }

            x -= arrow.Length;
        }

        int maxHeight = y;

        while (true)
        {
            if (!Console.KeyAvailable)
                continue;

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();

                // return item position
                return y - (maxHeight - 1);
            }

            if (key.Key == ConsoleKey.UpArrow && y != maxHeight)
            {
                ClearArrow();

                y--;
            }

            if (key.Key == ConsoleKey.DownArrow && y != (items - 1) + maxHeight)
            {
                ClearArrow();

                y++;
            }

            Console.SetCursorPosition(x, y);

            Console.Write(arrow);
        }
    }

}
