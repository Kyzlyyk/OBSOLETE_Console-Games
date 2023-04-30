namespace Snake;

using Draw;
using ToolBar;

class Program
{
    private static readonly int _windowWidth = 100;
    private static readonly int _windowHeight = 30;

    static void Main()
    {
        Console.SetWindowSize(_windowWidth, _windowHeight);
        Console.SetBufferSize(_windowWidth, _windowHeight);

        Console.CursorVisible = false;


        int item = MenuBar.Menu(new string[] { "Play", "Exit" }, x: Console.WindowWidth / 2, y: Console.WindowHeight / 2);

        int gameSpeed = 15;

        if (item == 1)
        {
            int itemScale = MenuBar.Menu(new string[] { "15x", "10x", "5x" }, signLabel: new string[] { "Game Speed" },
                x: Console.WindowWidth / 2, y: Console.WindowHeight / 2);

            if (itemScale == 1)
                gameSpeed = 3;
            
            if (itemScale == 2)
                gameSpeed = 6;
            
            if (itemScale == 3)
                gameSpeed = 8;
        }

        if (item == 2)
        {
            return;
        }

        DrawBorder();

        new GameController(gameSpeed).Start();
    }

    static void DrawBorder()
    {
        int borderTop = 3;
        int borderBottom = _windowHeight - borderTop;

        int borderLeft = 3;
        int borderRight = _windowWidth - borderLeft;

        new Figure(Console.WindowWidth / 2, Console.WindowHeight, 22, ConsoleColor.White).DrawSquare();
    }
}
