// See https://aka.ms/new-console-template for more information

using Demo;
using Teko.Core;
using Teko.Graphics;

var game = new Game();
game.AddService(new GraphicsService());
game.Scene = new TestScene();
game.Run();
