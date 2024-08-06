
using SFML.Graphics;

namespace Teko.Core;

public class RectF(Vector2f position, Vector2f size)
{
    public Vector2f Position = position, Size = size;

    public float Left => Position.X;
    public float Right => Position.X + Size.X;
    
    public float Top => Position.Y;
    public float Bottom => Position.Y + Size.Y;

    internal FloatRect ToSfmlRect()
        => new(Position.ToSfmlVec(), Size.ToSfmlVec());
    
    public RectI ToInt()
        => new(Position.ToInt(), Size.ToInt());
    
    public RectF(float x, float y, float width, float height) : this(new(x, y), new(width, height)) { }
}