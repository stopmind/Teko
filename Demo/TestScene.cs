using Teko.Core;
using Teko.Graphics;
using Teko.Input;
using Teko.Resources;

namespace Demo;

public class TestScene : Scene
{
    private GraphicsService? _graphics;
    private Input? _input;
    private Texture? _texture;
    private View _view = new(Vector2f.Zero, Vector2f.One);
    private float _speed = 160f;
    
    public override void Ready()
    {
        _graphics = Game.GetService<GraphicsService>();
        _graphics.SetLayersCount(6);
        _graphics.SetLayersCount(2);
        
        var resources = Game.GetService<ResourcesLoader>();
        _texture = resources.LoadResource<Texture>("A.png");
        
        _input = Game.GetService<Input>();
        _input.SetKeyboardEvent("playerUp", Key.W);
        _input.SetKeyboardEvent("playerLeft", Key.A);
        _input.SetKeyboardEvent("playerDown", Key.S);
        _input.SetKeyboardEvent("playerRight", Key.D);
        
        _view.Size = _graphics!.GetSize().ToFloat();

        for (var i = 0; i < 10000; i++)
        {
            var vec2 = new Vector2f(i % 5, 4);
            Console.WriteLine(vec2 * 2);
        }
    }

    public override void Update(float delta)
    {
        var move = new Vector2f(0, 0);

        if (_input!.IsDown("playerUp"))    move.Y--;
        if (_input!.IsDown("playerDown"))  move.Y++;
        if (_input!.IsDown("playerLeft"))  move.X--;
        if (_input!.IsDown("playerRight")) move.X++;
        
        _view.Center += move.Normalize() * delta * _speed;
        _view.Size = _graphics!.GetSize().ToFloat();
        _graphics!.GetLayer(0).View = _view;
    }

    public override void Draw(float delta)
    {
        _graphics!.SetCurrentLayer(0);
        _graphics!.DrawSprite(Vector2f.Zero, _texture!, scale: new Vector2f(2));
        _graphics!.SetCurrentLayer(1);
        _graphics!.DrawSprite(_graphics!.GetSize().ToFloat() / 2 - _texture!.Size.ToFloat(), _texture!, scale: new Vector2f(2));
    }

    public override void OnClose()
    {
        Game.Exit();
    }
}