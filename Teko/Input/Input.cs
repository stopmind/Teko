using SFML.Window;
using Teko.Core;

namespace Teko.Input;

public class Input : AService
{
    private Dictionary<string, InputEvent> _events = new();
    protected override void OnSetup()
    {
        GameInner.UpdateEvent += (_) =>
        {
            foreach (var (_, inputEvent) in _events)
                inputEvent.Update();
        };
    }

    public void SetKeyboardEvent(string id, Key key)
        => _events[id] = new InputEvent(() => !Keyboard.IsKeyPressed((Keyboard.Key)key));
    
    public void SetMouseEvent(string id, MouseButton button)
        => _events[id] = new InputEvent(() => !Mouse.IsButtonPressed((Mouse.Button)button));

    public bool IsUp(string id)
        => _events[id].State == InputState.Up;
    
    public bool IsDown(string id)
        => _events[id].State == InputState.Down;
    
    public bool IsPressed(string id)
        => _events[id].State == InputState.Pressed;
    
    public Vector2i GetMousePos() => new(Mouse.GetPosition(GameInner.Backend.Window));
}