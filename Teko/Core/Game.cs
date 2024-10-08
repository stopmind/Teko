using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Teko.Inject;

namespace Teko.Core;

public class Game : ISource
{
    private readonly GameInner _inner;
    private readonly Backend _backend;
    private readonly Dictionary<Type, AService> _services = new();
    private readonly Injector _injector;

    private Scene? _scene;
    
    public Scene? Scene
    {
        get => _scene;
        set
        {
            _scene = value;
#if !DEBUG
            try
            {
#endif
                if (_scene != null)
                {
                    _injector.Inject(_scene);
                    _scene.Setup(this);
                }
#if !DEBUG
            }
            catch
            {
                Exit();
                throw;
            }
#endif
        }
    }

    private void Update(float delta)
    {
        Scene?.Update(delta);
        _inner.CallUpdate(delta);
    }
    
    private void Draw(float delta)
    {
        Scene?.Draw(delta);
        _inner.CallDraw(delta);
    }
    
    public void Run()
    {
        var window = _backend.Window;
        
        window.SetFramerateLimit(120);

        window.Closed += (_, _) => _scene?.OnClose();

        var deltaClock = new Clock();
        try
        {
            while (_backend.Window.IsOpen)
            {
                var delta = deltaClock.ElapsedTime.AsSeconds();
                deltaClock.Restart();

                Update(delta);
                Draw(delta);

                window.Display();
                window.DispatchEvents();
            }
        }
        catch
        {
            Exit();
            throw;
        }
    }

    public void Exit()
    {
        _inner.CallExit();
        _backend.Window.Close();
    }

    public void AddService(AService aService)
    {
        if (!_services.TryAdd(aService.GetType(), aService))
            throw new Exception("Failed add service");
        
        _injector.Inject(aService);
        aService.Setup(this, _inner);
    }
    
    public TService? TryGetService<TService>() where TService : AService
    {
        _services.TryGetValue(typeof(TService), out var service);
        return (TService?)service;
    }
    
    public TService GetService<TService>() where TService : AService
    {
        if (_services.TryGetValue(typeof(TService), out var service))
            return (TService)service;
        
        throw new Exception("Failed get service");
    }
    
    public Game(uint width, uint height, string title)
    {
        _backend = new Backend(new RenderWindow(new VideoMode(width, height), title));
        _inner = new GameInner(_backend);
        _injector = new Injector([this]);
    }

    public object? GetValue(Type type)
    {
        if (type == typeof(Scene))
            return Scene;

        if (type.BaseType == typeof(AService))
            if (_services.TryGetValue(type, out var service))
                return service;
            else return null;

        return null;
    }
}