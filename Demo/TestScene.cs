using SFML.Graphics;
using SFML.System;
using Teko.Core;
using Teko.Graphics;
using Teko.Inject;
using Teko.Input;
using Teko.Registry;
using Teko.Resources;
using Teko.Storage;

namespace Demo;

public class TestScene : Scene
{
    [Inject] private GraphicsService? _graphics;
    [Inject] private Storage? _storage;
    [Inject] private Input? _input;
    private Texture? _texture;
    private Font? _font;
    private View _view = new(Vector2fC.Zero, Vector2fC.One);
    private float _speed = 160f;
    
    public override void Ready()
    {
        const string fileName = ".key";
        const string testKey = "SomeKey";
        
        Console.WriteLine(_storage!.IsFileCrypted(fileName));
        _storage.SetFileCrypted(fileName, true);
        Console.WriteLine($"{_storage.ReadFileText(fileName)}/{testKey}");
        _storage.WriteFileText(fileName, testKey);
        
        _graphics!.SetLayersCount(6);
        _graphics.SetLayersCount(2);
        
        var resources = Game.GetService<ResourcesLoader>();
        var load = resources.LoadResource<LoadConfig>("load.json");
        _texture = resources.LoadResource<Texture>("A.png");
        _font = resources.LoadResource<Font>("Monocraft.ttf");
        _font!.SetSmooth(false);
        
        _input!.SetKeyboardEvent("playerUp", Key.W);
        _input.SetKeyboardEvent("playerLeft", Key.A);
        _input.SetKeyboardEvent("playerDown", Key.S);
        _input.SetKeyboardEvent("playerRight", Key.D);
        
        _view.Size = _graphics!.GetSize().ToFloat();
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
        _graphics!.DrawSprite(Vector2fC.Zero, _texture!, scale: new Vector2f(2, 2));
        _graphics!.SetCurrentLayer(1);
        _graphics!.DrawSprite(_graphics!.GetSize().ToFloat() / 2 - _texture!.Size.ToFloat(), _texture!, scale: new Vector2f(2, 2));
        _graphics!.DrawText(Vector2fC.Zero, _font!, $"X: {_view.Center.X:0.0}\nY: {_view.Center.Y:0.0}", 32);
    }

    public override void OnClose()
    {
        Game.Exit();
    }
}