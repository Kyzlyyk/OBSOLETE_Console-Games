using System.ComponentModel;
using ToolBar;
using static System.Console;

namespace FlappyBird;

class Program
{
    private const int _windowWidth = 70;
    private const int _windowHeight = 30;

    static void Main()
    {
        SetWindowSize(_windowWidth, _windowHeight);
        SetBufferSize(_windowWidth, _windowHeight);

        CursorVisible = false;

        Title = "Flappy Bird <○>";

        int item = MenuBar.Menu(new string[] { "Start", "Exit" }, x: WindowWidth / 2, y: WindowHeight / 2);

        if (item == 2)
            Environment.Exit(0);

        new GameController().Start();
    }
}
