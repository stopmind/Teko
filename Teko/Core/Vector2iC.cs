using SFML.System;

namespace Teko.Core;

public static class Vector2iC
{
    public static Vector2i Up    = new( 0, -1);
    public static Vector2i Down  = new( 0,  1);
    public static Vector2i Left  = new(-1,  0);
    public static Vector2i Right = new( 1,  0);
    
    public static Vector2i One  = new(1, 1);
    public static Vector2i Zero = new(0, 0);
}