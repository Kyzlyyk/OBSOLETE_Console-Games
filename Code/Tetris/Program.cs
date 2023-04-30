namespace Tetris;

using Draw;
using ToolBar;
using static System.Console;

class Program
{
    private static readonly int _windowWidth = 87;
    private static readonly int _windowHeight = 50;

    private static readonly Thread _playSong = new(PlaySong);

    static void Main()
    {
        SetWindowSize(_windowWidth, _windowHeight);
        SetBufferSize(_windowWidth, _windowHeight);

        SetWindowPosition(0, 0);

        CursorVisible = false;

        int item = MenuBar.Menu(new string[] { "Play", "Exit" }, x: WindowWidth / 2, y: WindowHeight / 2);

        if (item == 0)
            return;

        item = MenuBar.Menu(new string[] { "1x", "5x", "10x" }, new string[] { "Game speed" }, Console.WindowWidth / 2, Console.WindowHeight / 2);

        int gameSpeed = 1;

        if (item == 1)
            gameSpeed = 150;
        if (item == 2) 
            gameSpeed = 100;
        if (item == 3)
            gameSpeed = 50;

        _playSong.Start();

        new GameController(gameSpeed).Start();
    }

    private static void PlaySong()
    {
        Beep(1320, 500); Beep(990, 250); Beep(1056, 250); Beep(1188, 250); Beep(1320, 125); Beep(1188, 125); Beep(1056, 250); Beep(990, 250); Beep(880, 500); Beep(880, 250); Beep(1056, 250); Beep(1320, 500); Beep(1188, 250); Beep(1056, 250); Beep(990, 750); Beep(1056, 250); Beep(1188, 500); Beep(1320, 500); Beep(1056, 500); Beep(880, 500); Beep(880, 500); System.Threading.Thread.Sleep(250); Beep(1188, 500); Beep(1408, 250); Beep(1760, 500); Beep(1584, 250); Beep(1408, 250); Beep(1320, 750); Beep(1056, 250); Beep(1320, 500); Beep(1188, 250); Beep(1056, 250); Beep(990, 500); Beep(990, 250); Beep(1056, 250); Beep(1188, 500); Beep(1320, 500); Beep(1056, 500); Beep(880, 500); Beep(880, 500); Thread.Sleep(500); PlaySong();
    }

}