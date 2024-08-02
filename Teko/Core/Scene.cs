namespace Teko.Core;

public abstract class Scene
{
    private Game? _game;

    protected Game Game => _game!;

    internal void Setup(Game game)
    {
        _game = game;
        Ready();
    }
    
    public abstract void Ready();
    public abstract void Update();
    public abstract void Draw();

    public abstract void OnClose();
}