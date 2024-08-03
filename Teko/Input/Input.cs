using Teko.Core;

namespace Teko.Input;

public class Input : AService
{
    private Dictionary<string, InputEvent> _events = new();
    protected override void OnSetup()
    {
        GameInner.UpdateEvent += () =>
        {
            foreach (var (_, inputEvent) in _events)
                inputEvent.Update();
        };
    }

    public void SetEvent(string id, Key key)
        => _events[id] = new InputEvent(InputState.Up, key);

    public bool IsUp(string id)
        => _events[id].State == InputState.Up;
    
    public bool IsDown(string id)
        => _events[id].State == InputState.Down;
    
    public bool IsPressed(string id)
        => _events[id].State == InputState.Pressed;
}