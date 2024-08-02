using SFML.Graphics;
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
            _scene?.Setup(this);
        }
    }

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

            //window.Clear(Color.Green);
            
            Draw();
            
            window.Display();
            window.DispatchEvents();
        }
    }

    public void AddService(AService aService)
    {
        if (!_services.TryAdd(aService.GetType(), aService))
            throw new Exception("Failed add service");
        
        aService.Setup(_inner);
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
    
    public Game()
    {
        _backend = new Backend(new RenderWindow(new VideoMode(300, 300), "TEKO"));
        _inner = new GameInner(_backend);
    }
}