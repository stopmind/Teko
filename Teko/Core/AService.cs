namespace Teko.Core;

public abstract class AService
{
    private GameInner? _gameInner;
    protected GameInner GameInner => _gameInner!;
    
    internal void Setup(GameInner inner)
    {
        _gameInner = inner;
        OnSetup();
    }

    protected abstract void OnSetup();
}