using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace GameOfLife;

internal static class Drawer
{
    public const char Pixel = '#';

    public static void DrawOnBoard(Vector2Int pos)
    {
        Console.SetCursorPosition(pos.X, pos.Y);
        Console.WriteLine(Pixel);
    }

    public static void Clear(Vector2Int pos)
    {
        Console.SetCursorPosition(pos.X, pos.Y);
        Console.WriteLine(" ");
    }
}

internal class Program
{
    static HashSet<Vector2Int> _aliveCells = new(Console.WindowHeight * Console.WindowWidth);

    static void CheckAroundCell(Vector2Int cell)
    {
        List<Cell> aliveCells = new(12);

        cell = new Vector2Int(cell.X - 1, cell.Y - 1);

        for (int i = 0; i < 3; i++)
        {
            aliveCells.Add(new Cell(cell, _aliveCells.Contains(cell), true));
            cell.X++;
        }
        for (int i = 0; i < 3; i++)
        {
            aliveCells.Add(new Cell(cell, _aliveCells.Contains(cell), true));
            cell.Y++;
        }
        for (int i = 0; i < 3; i++)
        {
            aliveCells.Add(new Cell(cell, _aliveCells.Contains(cell), true));
            cell.X--;
        }
        for (int i = 0; i < 3; i++)
        {
            aliveCells.Add(new Cell(cell, _aliveCells.Contains(cell), true));
            cell.Y--;
        }

        for (int i = 0; i < aliveCells.Count; i++)
        {
            if (CheckStatus(aliveCells.Count, aliveCells[i].Position))
                return;

            CheckAroundCell(aliveCells[i].Position);
        }
    }

    static bool CheckStatus(int alivesCountAroundCell, Vector2Int cellPosition)
    {
        if (alivesCountAroundCell == 2 || alivesCountAroundCell == 3)
            return false;

        else if (alivesCountAroundCell == 1 || alivesCountAroundCell == 0 || alivesCountAroundCell >= 4)
        {
            Drawer.Clear(cellPosition);
            _aliveCells.Remove(cellPosition);

            return false;
        }

        return true;
    }

    static void Main()
    {
        Vector2Int center = new(Console.WindowWidth / 2, Console.WindowHeight);

        _aliveCells.Add(center);

        Drawer.DrawOnBoard(center);

        while (true)
        {
            foreach (Vector2Int cell in _aliveCells)
            {
                CheckAroundCell(cell);  
            }
        }
    }
}
