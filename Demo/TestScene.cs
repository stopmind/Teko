using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

namespace Demo;

public class TestScene : Scene
{
    private GraphicsService? _graphics;
    private Texture? _texture;
    
    public override void Ready()
    {
        _graphics = Game.GetService<GraphicsService>();
        _texture = Game.GetService<ResourcesLoader>().LoadResource<Texture>("A.png");
    }

    public override void Update(float delta)
    {
        
    }

    public override void Draw(float delta)
    {
        _graphics!.DrawRect(new Rect(0, 0, 58, 92), Color.White, _texture);
    }

    public override void OnClose()
    {
        Game.Exit();
    }
}