namespace Teko.Core;

public class GameInner(Backend backend)
{
    public readonly Backend Backend = backend;

    public event Action? UpdateEvent;
    public event Action? DrawEvent;

    internal void CallUpdate() => UpdateEvent?.Invoke();
    internal void CallDraw() => DrawEvent?.Invoke();
}