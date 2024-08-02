using Teko.Core;
using Teko.Graphics;

namespace Demo;

public class TestScene : Scene
{
    private GraphicsService? _graphics;
    
    public override void Ready()
    {
        _graphics = Game!.GetService<GraphicsService>();
    }

    public override void Update()
    {
        
    }

    public override void Draw()
    {
        _graphics!.DrawRect(new Rect(0, 0, 20, 20), Color.White, null);
    }
}