using SFML.Graphics;
using Teko.Core;

namespace Teko.Graphics;

public class GraphicsService : AService
{
    private RectangleShape _rectShape = new();
    private Sprite _sprite = new();
    
    private DrawContext? _context;
    private List<DrawCall> _calls = new();
    private RenderTarget? _target;
    private readonly List<View?> _views = new();

    public Color FillColor = Color.Black;

    protected override void OnSetup() {
        GameInner.DrawEvent += Flush;
        _target = GameInner.Backend.Window;
    }

    public void SetContext(DrawContext? context) =>
        _context = context;

    public void SetLayersCount(int count)
    {
        for (var i = _views.Count; i < count; i++)
            _views.Add(null);
    }

    public void SetView(int layer, View? view)
        => _views[layer] = view;

    private void AddCall(Action func) =>
        _calls.Add(new DrawCall(_context, func));

    public void DrawRect(RectF rectF, Color color, Texture? texture)
    {
        AddCall(() =>
        {
            _rectShape.Position = rectF.Position.ToSfmlVec();
            _rectShape.Size = rectF.Size.ToSfmlVec();
            _rectShape.Texture = texture?.SfmlTexture;
            _rectShape.FillColor = color.ToSfmlColor();
            
            _target!.Draw(_rectShape);
        });
    }

    public void DrawSprite(Vector2f position, Color color, Texture texture)
    {
        AddCall(() =>
        {
            _sprite.Position = position.ToSfmlVec();
            _sprite.Color = color.ToSfmlColor();
            _sprite.Texture = texture.SfmlTexture;
            
            _target!.Draw(_sprite);
        });
    }

    public Vector2i GetSize()
        => new((int)_target!.Size.X, (int)_target!.Size.Y);
    
    private void Flush()
    {
        _target!.Clear(FillColor.ToSfmlColor());
        _context = null;

        var layers = _calls.GroupBy(call =>
        {
            if (call.Context != null)
                return call.Context.LayerIndex;
            return 0;
        }).ToDictionary(calls => calls.Key, calls => calls.ToArray());

        for (var i = 0; i < _views.Count; i++)
        {
            if (layers.ContainsKey(i))
                continue;
            
            var layerCalls = layers[i].ToList();
            layerCalls.Sort((call1, call2) =>
            {
                var x = 0f;
                if (call1.Context != null) x = call1.Context.ZIndex;
                var y = 0f;
                if (call2.Context != null) y = call2.Context.ZIndex;

                if (x > y)      return 1;
                else if (x < y) return -1;
                else            return 0;
            });
            
            _target.SetView(_views[i] == null ? _target.DefaultView : _views[i]!.ToSfmlView());
            
            foreach (var call in layerCalls)
                call.Func();
        }

        _calls.Clear();
    }
}