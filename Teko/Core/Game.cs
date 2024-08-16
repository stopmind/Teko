using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Teko.Core;

public class Game
{
    private readonly GameInner _inner;
    private readonly Backend _backend;
    private readonly Dictionary<Type, AService> _services = new();

    private Scene? _scene;
    
    public Scene? Scene
    {
        get => _scene;
        set
        {
            _scene = value;
            try
            {
                _scene?.Setup(this);
            }
            catch
            {
                Exit();
                throw;
            }
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
    }
}