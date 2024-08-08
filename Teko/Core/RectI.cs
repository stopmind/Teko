
using SFML.Graphics;

namespace Teko.Core;

public struct RectI(Vector2i position, Vector2i size)
{
    public Vector2i Position = position, Size = size;

    public int Left => Position.X;
    public int Right => Position.X + Size.X;
    
    public int Top => Position.Y;
    public int Bottom => Position.Y + Size.Y;

    internal IntRect ToSfmlRect()
        => new(Position.ToSfmlVec(), Size.ToSfmlVec());

    public RectF ToFloat()
        => new(Position.ToFloat(), Size.ToFloat());
    
    public override int GetHashCode()
    {
        return unchecked((int)( (uint)Left ^
                                ( ( (uint)Top << 13 ) | ( (uint)Top >> 19 ) ) ^
                                ( ( (uint)Size.X << 26 ) | ( (uint)Size.X >> 6 ) ) ^
                                ( ( (uint)Size.Y << 7 ) | ( (uint)Size.Y >> 25 ) ) ));
    }
    
    public RectI(int x, int y, int width, int height) : this(new(x, y), new(width, height)) { }
}