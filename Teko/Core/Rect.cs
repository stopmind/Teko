
namespace Teko.Core;

public class Rect(Vector2 position, Vector2 size)
{
    public Vector2 Position = position, Size = size;

    public float Left => Position.X;
    public float Right => Position.X + Size.X;
    
    public float Top => Position.Y;
    public float Bottom => Position.Y + Size.Y;
}