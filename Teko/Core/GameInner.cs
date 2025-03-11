using SFML.Graphics;

namespace Teko.Core;


public delegate void ActionWithDelta(float delta);
public class GameInner
{
    public RenderWindow? RenderWindow;

    public event ActionWithDelta? UpdateEvent;
    public event ActionWithDelta? DrawEvent;
    public event Action? ExitEvent;

    internal void CallUpdate(float delta) => UpdateEvent?.Invoke(delta);
    internal void CallDraw(float delta) => DrawEvent?.Invoke(delta);
    internal void CallExit() => ExitEvent?.Invoke();
}