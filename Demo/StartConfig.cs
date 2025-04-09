using SFML.Graphics;
using SFML.System;
using Teko.Resources;

namespace Demo;

public class StartConfig : AJsonResource
{
    public Texture Texture;
    public Vector2f StartPos;

    public StartConfig(Texture texture, Vector2f startPos)
    {
        Texture = texture;
        StartPos = startPos;
    }
}