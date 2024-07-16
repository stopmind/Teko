using SFML.System;

namespace Teko.Core;

public class Vector2(float x, float y)
{
    public float X = x, Y = y;
    
    public Vector2f ToSfmlVec() => new(X, Y);

    public static Vector2 operator+(Vector2 a, Vector2 b) =>
        new(a.X + b.X, a.Y + b.Y);
    
    public static Vector2 operator-(Vector2 a, Vector2 b) =>
        new(a.X - b.X, a.Y - b.Y);
    
    public static Vector2 operator*(Vector2 a, Vector2 b) =>
        new(a.X * b.X, a.Y * b.Y);
    
    public static Vector2 operator/(Vector2 a, Vector2 b) =>
        new(a.X / b.X, a.Y / b.Y);

    public Vector2(Vector2f sfmlVec) : this(sfmlVec.X, sfmlVec.Y) { }
}