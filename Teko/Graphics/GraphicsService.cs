using SFML.Graphics;
using SFML.System;
using Teko.Core;
using Vector2f = Teko.Core.Vector2f;
using Vector2i = Teko.Core.Vector2i;

namespace Teko.Graphics;

public class GraphicsService : AService
{
    private Layer[] _layers = [];
    private Layer? _currentLayer;

    public void SetCurrentLayer(uint index)
        => _currentLayer = _layers[index];

    public void SetLayersCount(uint count)
    {
        var newLayers = new Layer[count];

        for (var i = 0; i < Math.Min(_layers.Length, count); i++)
            newLayers[i] = _layers[i];

        for (var i = _layers.Length; i < count; i++)
            newLayers[i] = new Layer(
                GameInner.Backend.Window.Size.X,
                GameInner.Backend.Window.Size.Y
            );
        
        _layers = newLayers;
    }

    public Layer GetLayer(uint index)
        => _layers[index];

    private RectangleShape _rect = new();
    private Sprite _sprite = new();

    public void DrawRect(RectF rect, Color? color = null, Texture? texture = null, RectI? texRect = null)
    {
        _rect.FillColor = (color ?? Color.White).ToSfmlColor();
        _rect.Texture = texture?.SfmlTexture;
        _rect.Position = rect.Position.ToSfmlVec();
        _rect.Size = rect.Position.ToSfmlVec();
        if (texture != null)
            _rect.TextureRect = (texRect ?? new RectI(Vector2i.Zero, texture.Size)).ToSfmlRect();
        _currentLayer?.Texture.Draw(_rect);
    }

    public void DrawSprite(Vector2f position, Texture texture, Color? color = null, Vector2f? scale = null, RectI? texRect = null)
    {
        _sprite.Position = position.ToSfmlVec();
        _sprite.Texture = texture.SfmlTexture;
        _sprite.Color = (color ?? Color.White).ToSfmlColor();
        _sprite.Scale = (scale ?? Vector2f.One).ToSfmlVec();
        _sprite.TextureRect = (texRect ?? new RectI(Vector2i.Zero, texture.Size)).ToSfmlRect();
        _currentLayer?.Texture.Draw(_sprite);
    }

    public Vector2i GetSize()
        => new(GameInner.Backend.Window.Size);

    public Color FillColor = Color.Black;
    
    protected override void OnSetup()
    {
        SetLayersCount(1);
        SetCurrentLayer(0);
        
        var window = GameInner.Backend.Window;
        var layerSprite = new Sprite();
        
        window.Resized += (_, args) =>
        {
            var size = new SFML.System.Vector2f(args.Width, args.Height);
            window.SetView(new SFML.Graphics.View(size / 2, size));
            foreach (var layer in _layers)
                layer.Resize(args.Width, args.Height);
            layerSprite.TextureRect = new IntRect(0, 0, (int)args.Width, (int)args.Height);
        };
        
        GameInner.DrawEvent += _ =>
        {
            window.Clear(FillColor.ToSfmlColor());
            foreach (var layer in _layers)
            {
                layer.Texture.Display();
                layerSprite.Texture = layer.Texture.Texture;
                window.Draw(layerSprite);
                layer.Texture.Clear(SFML.Graphics.Color.Transparent);
            }
        };
    }
}