using static System.Console;

namespace Mario;

class Program
{
    private const int _windowWidth = 120;
    private const int _windowHeight = 30;

    static void Main()
    {
        SetWindowSize(_windowWidth, _windowHeight);
        SetBufferSize(_windowWidth, _windowHeight);

        CursorVisible = false;

        new GameController().Start();
    }
}
