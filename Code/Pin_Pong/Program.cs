namespace Pin_Pong;

using Draw;
using ToolBar;

class Program
{
    private static int windowWidth = 85; // 79
    private static int windowHeight = 23;

    private static int borderLeft = 3;
    private static int borderRight = windowWidth - 2;

    static void Main()
    {
        Console.SetWindowSize(windowWidth, windowHeight);
        Console.SetBufferSize(windowWidth, windowHeight);

        Console.CursorVisible = false;

        int item = MenuBar.Menu(itemLabel: new string[] { "Single Player", "MultiPlayer", "Exit" }, x: Console.WindowWidth / 2, y: Console.WindowHeight / 2);

        DrawBorder();

        if (item == 1)
        {
            new GameController(isMultiPlayer: false).Start();
        }

        if (item == 2)
        {
            new GameController(isMultiPlayer: true).Start();
        }

        if (item == 3)
        {
            Console.Clear();

            Console.ResetColor();

            return;
        }

    }

    static void DrawBorder()
    {
        // left
        for (int y = 1; y < windowHeight - 1; y++)
        {
            new Pixel(borderLeft, y, ConsoleColor.Magenta).Draw();
        }

        // top
        for (int x = borderLeft + 1; x < windowWidth - 1; x++)
        {
            new Pixel(x, 1, ConsoleColor.DarkMagenta, '▀').Draw();
        }

        // down
        for (int x = borderLeft; x < windowWidth - 1; x++)
        {
            new Pixel(x, windowHeight - 1, ConsoleColor.DarkCyan, '▀').Draw();
        }

        // right
        for (int y = 1; y < windowHeight - 1; y++)
        {
            new Pixel(borderRight, y, ConsoleColor.DarkCyan).Draw();
        }
    }
}