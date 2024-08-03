using SFML.Window;

namespace Teko.Input;

internal class InputEvent
{
    private Key _key;
    public InputState State;

    public void Update()
    {
        var newState = Keyboard.IsKeyPressed((Keyboard.Key)_key) ? InputState.Down : InputState.Up;

        if (State == InputState.Down && newState == InputState.Up) State = InputState.Pressed;
        else State = newState;
    }
    
    public InputEvent(InputState state, Key key)
    {
        State = state;
        _key = key;
    }
}