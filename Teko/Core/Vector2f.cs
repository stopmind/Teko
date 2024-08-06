using SFML.System;

namespace Teko.Core;

public class Vector2f(float x, float y)
{
    public static readonly Vector2f Zero  = new(0);
    public static readonly Vector2f One   = new(1);
    public static readonly Vector2f Up    = new(0, -1);
    public static readonly Vector2f Down  = new(0, 1);
    public static readonly Vector2f Right = new(1, 0);
    public static readonly Vector2f Left  = new(-1, 0);
    
    public float X = x, Y = y;
    
    internal SFML.System.Vector2f ToSfmlVec() => new(X, Y);

    public static Vector2f operator+(Vector2f a, Vector2f b) =>
        new(a.X + b.X, a.Y + b.Y);
    
    public static Vector2f operator-(Vector2f a, Vector2f b) =>
        new(a.X - b.X, a.Y - b.Y);
    
    public static Vector2f operator*(Vector2f a, Vector2f b) =>
        new(a.X * b.X, a.Y * b.Y);
    
    public static Vector2f operator*(Vector2f a, float b) =>
        new(a.X * b, a.Y * b);
    
    public static Vector2f operator/(Vector2f a, Vector2f b) =>
        new(a.X / b.X, a.Y / b.Y);
    
    public static Vector2f operator/(Vector2f a, float b) =>
        new(a.X / b, a.Y / b);

    public static bool operator ==(Vector2f a, Vector2f b)
        => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Vector2f a, Vector2f b) 
        => a.X != b.X || a.Y != b.Y;

    public Vector2f Normalize()
    {
        if (this == Zero)
            return Zero;
        
        var sum = MathF.Abs(X) + MathF.Abs(Y);
        return new Vector2f(X / sum, Y / sum);
    }

    internal Vector2f(SFML.System.Vector2f sfmlVec) : this(sfmlVec.X, sfmlVec.Y) { }
    public Vector2f(float a) : this(a, a) { }
}