namespace Teko.Core;

public class GameInner
{
    internal readonly Backend Backend;

    public event Action? UpdateEvent;
    public event Action? DrawEvent;

    internal void CallUpdate() => UpdateEvent?.Invoke();
    internal void CallDraw() => DrawEvent?.Invoke();
    
    internal GameInner(Backend backend)
    {
        Backend = backend;
    }
}