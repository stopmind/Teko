using SFML.System;

namespace Teko.Core;

public static class Vector2fC
{
    public static Vector2f Up    = new( 0, -1);
    public static Vector2f Down  = new( 0,  1);
    public static Vector2f Left  = new(-1,  0);
    public static Vector2f Right = new( 1,  0);
    
    public static Vector2f One  = new(1, 1);
    public static Vector2f Zero = new(0, 0);
}