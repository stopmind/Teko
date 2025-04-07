using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Teko.Inject;

namespace Teko.Core;

public static class Game
{
    private static GameInner _inner = new();
    private static Dictionary<Type, AService> _services = new();
    private static readonly Injector Injector = new([new ServicesSource()]);

    private static Scene? _scene;
    
    public static Scene? Scene
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
                    Injector.Inject(_scene);
                    _scene.Ready();
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

    public static string Title { get; private set; } = "";

    private static void Update(float delta)
    {
        Scene?.Update(delta);
        _inner.CallUpdate(delta);
    }
    
    private static void Draw(float delta)
    {
        Scene?.Draw(delta);
        _inner.CallDraw(delta);
    }
    
    public static void Run()
    {
        var window = _inner.RenderWindow;
        
        if (window == null)
            return;
        
        window.SetFramerateLimit(60);

        window.Closed += (_, _) => _scene?.OnClose();

        var deltaClock = new Clock();
        try
        {
            while (window.IsOpen)
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

    public static void Exit()
    {
        _inner.CallExit();
        _inner.RenderWindow?.Close();
    }

    public static void AddService(AService aService)
    {
        if (!_services.TryAdd(aService.GetType(), aService))
            throw new Exception("Failed add service");
        
        Injector.Inject(aService);
        aService.Setup(_inner);
    }
    
    public static TService? TryGetService<TService>() where TService : AService
    {
        _services.TryGetValue(typeof(TService), out var service);
        return (TService?)service;
    }

    public static AService? TryGetServiceByType(Type type)
    {
        _services.TryGetValue(type, out var service);
        return service;
    }
    
    public static TService GetService<TService>() where TService : AService
    {
        if (_services.TryGetValue(typeof(TService), out var service))
            return (TService)service;
        
        throw new Exception("Failed get service");
    }

    public static void InitWindow(uint width, uint height, string title)
    {
        _inner.RenderWindow = new RenderWindow(new VideoMode(width, height), title);
        Title = title;
    }

    public static void Reset()
    {
        _inner = new GameInner();
        _services.Clear();
    }
}