using System;
using System.Collections.Generic;
using System.Linq;
namespace Pin_Pong;

using Draw;

enum TypeControl
{
    WASD,
    NumPad
}

enum Direction
{
    Right,
    Left,
}

class Player
{
    public Player(
        int x, int y, 
        TypeControl typeControl,
        ConsoleColor color = ConsoleColor.White, 
        char pixel = '█'
        ) 
    {
        X = x;
        Y = y;

        Color = color;
        _pixel = pixel;

        _controlType = typeControl;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public ConsoleColor Color { get; set; }

    private char _pixel;

    private TypeControl _controlType;

    private static int sizeOfPanel = 5;

    public Pixel[] PlayerParthes { get; set; } = new Pixel[sizeOfPanel];

    public void Spawn()
    {
        for (int i = 0; i < sizeOfPanel; i++)
        {
            PlayerParthes[i] = (new Pixel(X, /*for*/ Y - i + (sizeOfPanel / 2) /*aligment*/, Color));
        }

        foreach (var playerParth in PlayerParthes)
        {
            playerParth.Draw();
        }
    }

    private void Up()
    {
        if (PlayerParthes[^1].Y <= 2)
        {
            for (int i = 0; i < PlayerParthes.Length; i++)
            {
                PlayerParthes[i].Y++;
            }
        }

        for (int i = PlayerParthes.Length - 1; i > -1; i--)
        {
            PlayerParthes[i].Clear();

            PlayerParthes[i].Y--;
            PlayerParthes[i].Draw();
        }
    }

    private void Down()
    {
        if (PlayerParthes[0].Y >= Console.WindowHeight - 2)
        {
            for (int i = 0; i < PlayerParthes.Length; i++)
            {
                PlayerParthes[i].Y--;
            }
        }

        for (int i = 0; i < PlayerParthes.Length; i++)
        {
            PlayerParthes[i].Clear();

            PlayerParthes[i].Y++;
            PlayerParthes[i].Draw();
        }
    }

    public void Move()
    {
        if (!Console.KeyAvailable)
            return;

        ConsoleKeyInfo key = Console.ReadKey(true);

        // check control type
        if (_controlType == TypeControl.WASD)
        {
            switch (key.Key)
            {
                case ConsoleKey.W: Up(); break;
                case ConsoleKey.S: Down(); break;
            }
        }
        else if (_controlType == TypeControl.NumPad)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow: Up(); break;
                case ConsoleKey.DownArrow: Down(); break;
            }
        }

    }

    public void AIMove(Pixel ball, Direction direction, int rangeVision)
    {
        void Move()
        {
            if (Y <= ball.Y)
                Down();
            else if (Y >= ball.Y)
                Up();
        }

        if (direction == Direction.Right)
        {
            if (ball.X >= X - rangeVision)
            {
                Move();
            }
        }

        if (direction == Direction.Left)
        {
            if (ball.X <= X + rangeVision)
            {
                Move();
            }
        }
    }
}