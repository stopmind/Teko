using SFML.Window;

namespace Teko.Input;

internal class InputEvent(Func<bool> isUp)
{
    private readonly Func<bool> _isUp = isUp;
    public InputState State = InputState.Up;

    public void Update()
    {
        var newState = _isUp() ? InputState.Up : InputState.Down;

        if (State == InputState.Down && newState == InputState.Up) State = InputState.Pressed;
        else State = newState;
    }
}