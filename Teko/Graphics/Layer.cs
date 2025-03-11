using SFML.Graphics;

namespace Teko.Graphics;

public class Layer
{
    
    public RenderTexture Texture;
    public View? View
    {
        get => Texture.GetView();
        set => Texture.SetView(value);
    }
    
    internal void Resize(uint width, uint height)
    {
        Texture = new RenderTexture(width, height);
    }

    internal Layer(uint width, uint height)
    {
        Texture = new RenderTexture(width, height);
    }
}