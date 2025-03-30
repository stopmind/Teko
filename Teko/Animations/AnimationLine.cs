namespace Teko.Animations;

public class AnimationLine<TValue>(
    Dictionary<float, TValue> values,
    Action<TValue> setter,
    AnimationFunc<TValue> func) : IAnimationLine
{
    
    public void Update(float time)
        => setter(func(values, time));
}

public delegate TValue AnimationFunc<TValue>(Dictionary<float, TValue> values, float time);