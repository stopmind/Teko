using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Teko.Core;

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
                GameInner.RenderWindow!.Size.X,
                GameInner.RenderWindow.Size.Y
            );
        
        _layers = newLayers;
    }

    public Layer GetLayer(uint index)
        => _layers[index];

    private RectangleShape _rect = new();
    private Sprite _sprite = new();

    public void DrawRect(FloatRect rect, Color? color = null, Texture? texture = null, IntRect? texRect = null)
    {
        _rect.FillColor = color ?? Color.White;
        _rect.Texture = texture;
        _rect.Position = rect.Position;
        _rect.Size = rect.Size;
        if (texture != null)
            _rect.TextureRect = texRect ?? new IntRect(Vector2iC.Zero, texture.Size.ToInt());
        _currentLayer?.Texture.Draw(_rect);
    }

    public void DrawSprite(Vector2f position, Texture texture, Color? color = null, Vector2f? scale = null, IntRect? texRect = null)
    {
        _sprite.Position = position;
        _sprite.Texture = texture;
        _sprite.Color = color ?? Color.White;
        _sprite.Scale = scale ?? Vector2fC.One;
        _sprite.TextureRect = texRect ?? new IntRect(Vector2iC.Zero, texture.Size.ToInt());
        _currentLayer?.Texture.Draw(_sprite);
    }

    public void DrawText(Vector2f position, Font font, string content, uint size = 16, Color? color = null)
    {
        var text = new Text(content, font, size);
        text.Position = position;
        text.FillColor = color ?? Color.White;
        _currentLayer?.Texture.Draw(text);
    }

    public Vector2u GetSize()
        => GameInner.RenderWindow!.Size;

    public Color FillColor = Color.Black;

    public void SetFullscrean(bool isFullscrean)
    {
        var window = GameInner.RenderWindow;
        
        if (window == null)
            return;
        
        window.Close();
        if (isFullscrean)
            GameInner.RenderWindow = new RenderWindow(new VideoMode(), Game.Title, Styles.Fullscreen);
        else
            GameInner.RenderWindow = new RenderWindow(new VideoMode(), Game.Title);
    }
    
    protected override void OnSetup()
    {
        SetLayersCount(1);
        SetCurrentLayer(0);
        
        var window = GameInner.RenderWindow!;
        var layerSprite = new Sprite();
        
        window.Resized += (_, args) =>
        {
            var size = new Vector2f(args.Width, args.Height);
            window.SetView(new View(size / 2, size));
            foreach (var layer in _layers)
                layer.Resize(args.Width, args.Height);
            layerSprite.TextureRect = new IntRect(0, 0, (int)args.Width, (int)args.Height);
        };
        
        GameInner.DrawEvent += _ =>
        {
            window.Clear(FillColor);
            foreach (var layer in _layers)
            {
                layer.Texture.Display();
                layerSprite.Texture = layer.Texture.Texture;
                window.Draw(layerSprite);
                layer.Texture.Clear(Color.Transparent);
            }
        };
    }
}