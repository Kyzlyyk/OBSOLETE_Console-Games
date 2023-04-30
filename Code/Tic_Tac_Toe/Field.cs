namespace Tic_Tac_Toe;

using Draw;
using System;
using System.Drawing;

enum TicTacToe
{
    Tic,
    Tac,
    Null,
}

internal class Field
{
    public Field(
        int x, int y, int radius, ConsoleColor color = ConsoleColor.White)
    {
        X = x;
        Y = y;

        Color = color;
        _radius = radius;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public ConsoleColor Color { get; set; }

    public bool IsMarked { get; private set; }
    public TicTacToe TicTacToe { get; private set; } = TicTacToe.Null;

    private readonly int _radius;

    private int _borderLeft;
    private int _borderRight;

    private int _borderTop;
    private int _borderBottom;

    public void DrawField(ConsoleColor color = ConsoleColor.White)
    {
        Color = color;

        _borderTop = Y - _radius;
        _borderBottom = Y + _radius;

        _borderLeft = (X - _radius) * 2;
        _borderRight = (X + _radius) * 2;

        // left
        for (int i = _borderTop; i < _borderBottom; i++)
        {
            new Pixel(_borderLeft, i, color: color).Draw();
        }

        // bottom
        for (int i = _borderLeft; i < _borderRight + 1; i++)
        {
            new Pixel(i, _borderBottom, pixel: '▀', color: color).Draw();
        }

        // top
        for (int i = _borderLeft + 1; i < _borderRight; i++)
        {
            new Pixel(i, _borderTop, pixel: '▀', color: color).Draw();
        }

        // right
        for (int i = _borderTop; i < _borderBottom; i++)
        {
            new Pixel(_borderRight, i, color: color).Draw();
        }
    }

    public void MarkField(TicTacToe ticTac)
    {
        IsMarked = true;

        if (ticTac == TicTacToe.Tic)
        {
            for (int i = _borderTop; i < _borderBottom; i++)
            {
                for (int j = _borderLeft; j < _borderRight; j++)
                {
                    new Pixel(j++, i++, ConsoleColor.Red, pixel: '■').Draw();
                }
            }

            for (int i = _borderTop; i < _borderBottom; i++)
            {
                for (int j = _borderRight; j > _borderLeft; j--)
                {
                    new Pixel(j--, i++, ConsoleColor.Red, pixel: '■').Draw();
                }
            }

            TicTacToe = TicTacToe.Tic;
        }

        if (ticTac == TicTacToe.Tac)
        {
            new Figure(X, Y, _radius, ConsoleColor.Blue).DrawCircle();

            TicTacToe = TicTacToe.Tac;
        }
    }
}