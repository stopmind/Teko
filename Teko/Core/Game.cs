using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Teko.Inject;

namespace Teko.Core;

public static class Game
{
    private static readonly GameInner Inner = new();
    private static readonly Dictionary<Type, AService> Services = new();
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

    public static string Title { get; private set; }

    private static void Update(float delta)
    {
        Scene?.Update(delta);
        Inner.CallUpdate(delta);
    }
    
    private static void Draw(float delta)
    {
        Scene?.Draw(delta);
        Inner.CallDraw(delta);
    }
    
    public static void Run()
    {
        var window = Inner.RenderWindow;
        
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
        Inner.CallExit();
        Inner.RenderWindow?.Close();
    }

    public static void AddService(AService aService)
    {
        if (!Services.TryAdd(aService.GetType(), aService))
            throw new Exception("Failed add service");
        
        Injector.Inject(aService);
        aService.Setup(Inner);
    }
    
    public static TService? TryGetService<TService>() where TService : AService
    {
        Services.TryGetValue(typeof(TService), out var service);
        return (TService?)service;
    }

    public static AService? TryGetServiceByType(Type type)
    {
        Services.TryGetValue(type, out var service);
        return service;
    }
    
    public static TService GetService<TService>() where TService : AService
    {
        if (Services.TryGetValue(typeof(TService), out var service))
            return (TService)service;
        
        throw new Exception("Failed get service");
    }

    public static void InitWindow(uint width, uint height, string title)
    {
        Inner.RenderWindow = new RenderWindow(new VideoMode(width, height), title);
        Title = title;
    }
}