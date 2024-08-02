namespace Teko.Core;

public class GameInner
{
    internal readonly Backend Backend;

    public event Action? UpdateEvent;
    public event Action? DrawEvent;
    public event Action? ExitEvent;

    internal void CallUpdate() => UpdateEvent?.Invoke();
    internal void CallDraw() => DrawEvent?.Invoke();
    internal void CallExit() => ExitEvent?.Invoke();
    
    internal GameInner(Backend backend)
    {
        Backend = backend;
    }
}