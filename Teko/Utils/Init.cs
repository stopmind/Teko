using Teko.Core;
using Teko.Graphics;
using Teko.Registry;
using Teko.Resources;

namespace Teko.Utils;

public static class Init
{
    public static void Run(uint width, uint height, string title, Scene scene, string storageKey, string[]? packsPaths = null,
        string? logPath = null, AService[]? services = null)
    {
        Game.InitWindow(width, height, title);
        
        Game.AddService(new Logger(logPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "game.log")));
        Game.AddService(new Input.Input());
        Game.AddService(new GraphicsService());
        Game.AddService(new ResourcesLoader(packsPaths ?? [Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets")]));
        Game.AddService(new RegistryService());
        Game.AddService(new Storage.Storage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage"), storageKey));

        if (services != null)
        {
            foreach (var service in services)
                Game.AddService(service);
        }
        Game.Scene = scene;
        
        Game.Run();
    }
}