using SFML.Graphics;
using SFML.Window;

namespace Teko.Core;

public class Game
{
    private readonly GameInner _inner;
    private readonly Backend _backend;
    private readonly Dictionary<Type, Service> _services = new();
    
    public Scene? Scene;

    private void Update()
    {
        Scene?.Update();
        _inner.CallUpdate();
    }
    
    private void Draw()
    {
        Scene?.Draw();
        _inner.CallDraw();
    }
    
    public void Run()
    {
        var window = _backend.Window;
        
        window.SetFramerateLimit(120);

        window.Closed += (_,_) =>
        {
            window.Close();
        };
        
        while (_backend.Window.IsOpen)
        {
            Update();

            window.Clear(Color.Green);
            
            Draw();
            
            window.Display();
            window.DispatchEvents();
        }
    }

    public void AddService(Service service)
    {
        if (!_services.TryAdd(service.GetType(), service))
            throw new Exception("Failed add service");
        
        service.Setup(_inner);
    }
    
    public Service? TryGetService<TService>() where TService : Service
    {
        _services.TryGetValue(typeof(TService), out var service);
        return service;
    }
    
    public Service GetService<TService>() where TService : Service
    {
        if (_services.TryGetValue(typeof(TService), out var service))
            return service;
        
        throw new Exception("Failed get service");
    }
    
    public Game()
    {
        _backend = new Backend(new RenderWindow(new VideoMode(300, 300), "TEKO"));
        _inner = new GameInner(_backend);
    }
}