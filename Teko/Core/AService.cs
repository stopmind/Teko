namespace Teko.Core;

public abstract class AService
{
    private GameInner? _gameInner;
    private Game? _game;
    protected GameInner GameInner => _gameInner!;
    protected Game Game => _game!;
    
    internal void Setup(Game game, GameInner inner)
    {
        _game = game;
        _gameInner = inner;
        OnSetup();
    }

    protected abstract void OnSetup();
}