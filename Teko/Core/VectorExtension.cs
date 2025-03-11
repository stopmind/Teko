using SFML.System;

namespace Teko.Core;

public static class VectorExtension
{
    
    public static Vector2u ToUint(this Vector2i original)
        => new((uint)original.X, (uint)original.Y);
    
    public static Vector2f ToFloat(this Vector2i original)
        => new(original.X, original.Y);
    
    public static Vector2i ToInt(this Vector2u original)
        => new((int)original.X, (int)original.Y);
    public static Vector2f ToFloat(this Vector2u original)
        => new(original.X, original.Y);
    
    public static Vector2u ToUint(this Vector2f original)
        => new((uint)original.X, (uint)original.Y);
    
    public static Vector2i ToInt(this Vector2f original)
        => new((int)original.X, (int)original.Y);

    public static Vector2f Normalize(this Vector2f original)
    {
        if (original == Vector2fC.Zero)
            return Vector2fC.Zero;
        
        var sum = MathF.Abs(original.X) + MathF.Abs(original.Y);
        return new Vector2f(original.X / sum, original.Y / sum);
    }
}