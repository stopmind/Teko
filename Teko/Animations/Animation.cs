namespace Teko.Animations;

public class Animation(float duration, IAnimationLine[] lines)
{
    
    public readonly float Duration = duration;
    
    public void Update(float time)
    {
        foreach (var line in lines)
            line.Update(time);
    }
}