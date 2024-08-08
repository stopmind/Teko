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
    private Vector2f _pos = new(0,0);
    private float _speed = 40f;
    
    public override void Ready()
    {
        _graphics = Game.GetService<GraphicsService>();
        _graphics.SetLayersCount(2);
        _texture = Game.GetService<ResourcesLoader>().LoadResource<Texture>("A.png");
        
        _input = Game.GetService<Input>();
        _input.SetKeyboardEvent("playerUp", Key.W);
        _input.SetKeyboardEvent("playerLeft", Key.A);
        _input.SetKeyboardEvent("playerDown", Key.S);
        _input.SetKeyboardEvent("playerRight", Key.D);
    }

    public override void Update(float delta)
    {
        var move = new Vector2f(0, 0);

        if (_input!.IsDown("playerUp"))    move.Y--;
        if (_input!.IsDown("playerDown"))  move.Y++;
        if (_input!.IsDown("playerLeft"))  move.X--;
        if (_input!.IsDown("playerRight")) move.X++;
        
        _pos += move.Normalize() * delta * _speed;
    }

    public override void Draw(float delta)
    {
        _graphics!.SetContext(new DrawContext(0,0));
        _graphics!.DrawRect(new RectF(Vector2f.One * 25, new Vector2f(64, 64)), Color.White, _texture);
        _graphics!.SetContext(new DrawContext(1,0));
        _graphics!.DrawRect(new RectF(_pos, new Vector2f(64, 64)), Color.White, _texture);
    }

    public override void OnClose()
    {
        Game.Exit();
    }
}