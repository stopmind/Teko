namespace Teko.Core;

public abstract class Scene
{
    public abstract void Ready();
    public abstract void Update(float delta);
    public abstract void Draw(float delta);

    public abstract void OnClose();
}