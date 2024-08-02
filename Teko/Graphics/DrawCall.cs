namespace Teko.Graphics;

internal class DrawCall(DrawContext? context, Action func)
{
    public DrawContext? Context = context;
    public Action Func = func;
}