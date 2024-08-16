namespace Teko.Core;


public delegate void ActionWithDelta(float delta);
public class GameInner
{
    internal readonly Backend Backend;

    public event ActionWithDelta? UpdateEvent;
    public event ActionWithDelta? DrawEvent;
    public event Action? ExitEvent;

    internal void CallUpdate(float delta) => UpdateEvent?.Invoke(delta);
    internal void CallDraw(float delta) => DrawEvent?.Invoke(delta);
    internal void CallExit() => ExitEvent?.Invoke();
    
    internal GameInner(Backend backend)
    {
        Backend = backend;
    }
}