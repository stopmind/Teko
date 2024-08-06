namespace Teko.Core;

public class Vector2i(int x, int y)
{
    public static readonly Vector2i Zero  = new(0);
    public static readonly Vector2i One   = new(1);
    public static readonly Vector2i Up    = new(0, -1);
    public static readonly Vector2i Down  = new(0, 1);
    public static readonly Vector2i Right = new(1, 0);
    public static readonly Vector2i Left  = new(-1, 0);
    
    public int X = x, Y = y;
    
    internal SFML.System.Vector2i ToSfmlVec() => new(X, Y);

    public static Vector2i operator+(Vector2i a, Vector2i b) =>
        new(a.X + b.X, a.Y + b.Y);
    
    public static Vector2i operator-(Vector2i a, Vector2i b) =>
        new(a.X - b.X, a.Y - b.Y);
    
    public static Vector2i operator*(Vector2i a, Vector2i b) =>
        new(a.X * b.X, a.Y * b.Y);
    
    public static Vector2i operator*(Vector2i a, int b) =>
        new(a.X * b, a.Y * b);
    
    public static Vector2i operator/(Vector2i a, Vector2i b) =>
        new(a.X / b.X, a.Y / b.Y);
    
    public static Vector2i operator/(Vector2i a, int b) =>
        new(a.X / b, a.Y / b);

    public static bool operator ==(Vector2i a, Vector2i b)
        => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2i a, Vector2i b) 
        => a.X != b.X || a.Y != b.Y;

    internal Vector2i(SFML.System.Vector2i sfmlVec) : this(sfmlVec.X, sfmlVec.Y) { }
    public Vector2i(int a) : this(a, a) { }
}