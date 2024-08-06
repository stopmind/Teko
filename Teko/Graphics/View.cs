using Teko.Core;

namespace Teko.Graphics;

public class View(Rect rect, Vector2 center, Vector2 size, float rotation)
{
    public Rect Rect = rect;
    public Vector2 Center = center;
    public Vector2 Size = size;
    public float Rotation = rotation;
    
    internal SFML.Graphics.View ToSfmlView()
    {
        var view = new SFML.Graphics.View(Center.ToSfmlVec(), Size.ToSfmlVec());
        view.Viewport = Rect.ToSfmlRect();
        view.Rotation = Rotation;
        return view;
    }

    public View(Vector2 center, Vector2 size, float rotation = 0f) : this(new Rect(0f, 0f, 1f, 1f), center, size, rotation) { }
}