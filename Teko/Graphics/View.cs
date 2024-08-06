using Teko.Core;

namespace Teko.Graphics;

public class View(RectF rectF, Vector2f center, Vector2f size, float rotation)
{
    public RectF RectF = rectF;
    public Vector2f Center = center;
    public Vector2f Size = size;
    public float Rotation = rotation;
    
    internal SFML.Graphics.View ToSfmlView()
    {
        var view = new SFML.Graphics.View(Center.ToSfmlVec(), Size.ToSfmlVec());
        view.Viewport = RectF.ToSfmlRect();
        view.Rotation = Rotation;
        return view;
    }

    public View(Vector2f center, Vector2f size, float rotation = 0f) : this(new RectF(0f, 0f, 1f, 1f), center, size, rotation) { }
}