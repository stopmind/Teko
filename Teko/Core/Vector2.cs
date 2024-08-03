using SFML.System;

namespace Teko.Core;

public class Vector2(float x, float y)
{
    public static readonly Vector2 Zero  = new(0);
    public static readonly Vector2 One   = new(1);
    public static readonly Vector2 Up    = new(0, -1);
    public static readonly Vector2 Down  = new(0, 1);
    public static readonly Vector2 Right = new(1, 0);
    public static readonly Vector2 Left  = new(-1, 0);
    
    public float X = x, Y = y;
    
    internal Vector2f ToSfmlVec() => new(X, Y);

    public static Vector2 operator+(Vector2 a, Vector2 b) =>
        new(a.X + b.X, a.Y + b.Y);
    
    public static Vector2 operator-(Vector2 a, Vector2 b) =>
        new(a.X - b.X, a.Y - b.Y);
    
    public static Vector2 operator*(Vector2 a, Vector2 b) =>
        new(a.X * b.X, a.Y * b.Y);
    
    public static Vector2 operator*(Vector2 a, float b) =>
        new(a.X * b, a.Y * b);
    
    public static Vector2 operator/(Vector2 a, Vector2 b) =>
        new(a.X / b.X, a.Y / b.Y);
    
    public static Vector2 operator/(Vector2 a, float b) =>
        new(a.X / b, a.Y / b);

    public static bool operator ==(Vector2 a, Vector2 b)
        => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2 a, Vector2 b) 
        => a.X != b.X || a.Y != b.Y;

    public Vector2 Normalize()
    {
        if (this == Zero)
            return Zero;
        
        var sum = MathF.Abs(X) + MathF.Abs(Y);
        return new Vector2(X / sum, Y / sum);
    }

    internal Vector2(Vector2f sfmlVec) : this(sfmlVec.X, sfmlVec.Y) { }
    public Vector2(float a) : this(a, a) { }
}