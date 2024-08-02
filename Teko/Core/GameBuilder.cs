using System.Data;
using Teko.Graphics;
using Teko.Resources;

namespace Teko.Core;

public class GameBuilder
{
    private Scene? _scene;
    private uint _width, _height;
    private string _title;
    private bool _windowConfigured;
    private List<AService> _services = new();
    
    public GameBuilder Window(uint width, uint height, string title)
    {
        _width = width;
        _height = height;
        _title = title;
        _windowConfigured = true;
        return this;
    }

    public GameBuilder Scene(Scene scene)
    {
        _scene = scene;
        return this;
    }

    public GameBuilder AddService(AService service)
    {
        _services.Add(service);
        return this;
    }

    public GameBuilder StdServices(string[] packsPaths)
    {
        AddService(new GraphicsService());
        AddService(new ResourcesLoader(packsPaths));
        return this;
    }

    public Game Result()
    {
        if (!_windowConfigured)
            throw new Exception("Window not configured");
        
        if (_scene == null)
            throw new Exception("Scene not configured");
        
        var game = new Game(_width, _height, _title);
        foreach (var service in _services)
            game.AddService(service);
        game.Scene = _scene;

        return game;
    }

    public void Run()
        => Result().Run();
}