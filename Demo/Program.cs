// See https://aka.ms/new-console-template for more information

using Demo;
using Teko.Utils;

new GameBuilder()
    .StdServices(["Content"], "game.log")
    .Window(800, 450, "Teko Demo")
    .Scene(new TestScene())
    .Run();
