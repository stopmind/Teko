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
    private Vector2 _pos = new(0,0);
    private float _speed = 40f;
    
    public override void Ready()
    {
        _graphics = Game.GetService<GraphicsService>();
        _texture = Game.GetService<ResourcesLoader>().LoadResource<Texture>("A.png");
        
        _input = Game.GetService<Input>();
        _input.SetEvent("playerUp", Key.W);
        _input.SetEvent("playerLeft", Key.A);
        _input.SetEvent("playerDown", Key.S);
        _input.SetEvent("playerRight", Key.D);
    }

    public override void Update(float delta)
    {
        var move = new Vector2(0, 0);

        if (_input!.IsDown("playerUp"))    move.Y--;
        if (_input!.IsDown("playerDown"))  move.Y++;
        if (_input!.IsDown("playerLeft"))  move.X--;
        if (_input!.IsDown("playerRight")) move.X++;
        
        _pos += move.Normalazie() * delta * _speed;
    }

    public override void Draw(float delta)
    {
        _graphics!.DrawRect(new Rect(_pos, new Vector2(64, 64)), Color.White, _texture);
    }

    public override void OnClose()
    {
        Game.Exit();
    }
}