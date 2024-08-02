// See https://aka.ms/new-console-template for more information

using Demo;
using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

var game = new Game();
game.AddService(new GraphicsService());
game.AddService(new ResourcesLoader(["Content"]));
game.Scene = new TestScene();
game.Run();
