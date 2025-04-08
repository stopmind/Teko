using Teko.Core;
using Teko.Graphics;
using Teko.Registry;
using Teko.Resources;

namespace Teko.Utils;

public static class Init
{
    public static void Run(uint width, uint height, string title, Scene scene, string storageKey, string[]? packsPaths = null,
        string? logPath = null, AService[]? services = null, bool changeCwd = true)
    {
        if (changeCwd)
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        Game.InitWindow(width, height, title);
        
        Game.AddService(new Logger(logPath ?? "game.log"));
        Game.AddService(new Input.Input());
        Game.AddService(new GraphicsService());
        Game.AddService(new ResourcesLoader(packsPaths ?? ["Assets"]));
        Game.AddService(new RegistryService());
        Game.AddService(new Storage.Storage("Storage", storageKey));

        if (services != null)
        {
            foreach (var service in services)
                Game.AddService(service);
        }
        Game.Scene = scene;
        
        Game.Run();
    }
}