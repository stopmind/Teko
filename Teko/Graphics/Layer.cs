using SFML.Graphics;
using SFML.System;

namespace Teko.Graphics;

public class Layer
{
    private View? _view;
    internal RenderTexture Texture;
    public View? View
    {
        get => _view;
        set
        {
            _view = value;
            UpdateView();
        }
    }

    private void UpdateView()
        => Texture.SetView(_view?.ToSfmlView() ?? Texture.DefaultView);
    
    internal void Resize(uint width, uint height)
    {
        Texture = new RenderTexture(width, height);
        //UpdateView();
    }

    internal Layer(uint width, uint height)
    {
        Texture = new RenderTexture(width, height);
    }
}