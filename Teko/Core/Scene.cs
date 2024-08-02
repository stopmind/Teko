namespace Teko.Core;

public abstract class Scene
{
    protected Game? Game;

    internal void Setup(Game game)
    {
        Game = game;
        Ready();
    }
    
    public abstract void Ready();
    public abstract void Update();
    public abstract void Draw();
}