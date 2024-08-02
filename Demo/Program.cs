// See https://aka.ms/new-console-template for more information

using Demo;
using Teko.Core;

new GameBuilder()
    .StdServices(["Content"])
    .Window(800, 450, "Teko Demo")
    .Scene(new TestScene())
    .Run();
