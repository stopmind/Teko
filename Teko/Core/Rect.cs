
using SFML.Graphics;

namespace Teko.Core;

public class Rect(Vector2 position, Vector2 size)
{
    public Vector2 Position = position, Size = size;

    public float Left => Position.X;
    public float Right => Position.X + Size.X;
    
    public float Top => Position.Y;
    public float Bottom => Position.Y + Size.Y;

    internal FloatRect ToSfmlRect()
        => new(Position.ToSfmlVec(), Size.ToSfmlVec());
    
    public Rect(float x, float y, float width, float height) : this(new(x, y), new(width, height)) { }
}