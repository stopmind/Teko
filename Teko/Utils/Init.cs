using Teko.Core;
using Teko.Graphics;
using Teko.Registry;
using Teko.Resources;

namespace Teko.Utils;

public static class Init
{
    public static void Run(uint width, uint height, string title, Scene scene, string[]? packsPaths = null, string logPath = "game.log", AService[]? services = null)
    {
        Game.InitWindow(width, height, title);
        
        Game.AddService(new Logger(logPath));
        Game.AddService(new Input.Input());
        Game.AddService(new GraphicsService());
        Game.AddService(new ResourcesLoader(packsPaths ?? ["Assets"]));
        Game.AddService(new RegistryService());

        if (services != null)
        {
            foreach (var service in services)
                Game.AddService(service);
        }
        Game.Scene = scene;
        
        Game.Run();
    }
}