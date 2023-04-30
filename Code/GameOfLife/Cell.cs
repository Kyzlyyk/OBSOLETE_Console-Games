namespace GameOfLife;

public struct Cell
{
    public Cell(Vector2Int position, bool isAlive, bool isChecked)
    {
        Position = position;
        IsAlive = isAlive;
        IsChecked = isChecked;
    }

    public Vector2Int Position { get; set; }
    public bool IsAlive { get; set; }
    public bool IsChecked { get; set; } 
}

public struct Vector2Int
{
    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int operator -(Vector2Int l, Vector2Int r)
    {
        return new Vector2Int(l.X - r.X, l.Y - r.Y);
    }
    
    public static Vector2Int operator +(Vector2Int l, Vector2Int r)
    {
        return new Vector2Int(l.X + r.X, l.Y + r.Y);
    }

    public static bool operator ==(Vector2Int l, Vector2Int r)
    {
        return l.X == r.X && l.Y == r.Y;
    }
    
    public static bool operator !=(Vector2Int l, Vector2Int r)
    {
        return l.X != r.X || l.Y != r.Y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public int X, Y;
}