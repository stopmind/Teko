namespace Teko.Animations;

public class AnimationBuilder
{
    private List<IAnimationLine> _lines = new();
    private float _duration;

    public AnimationBuilder NewLine<TValue>(Action<TValue> setter, Dictionary<float, TValue> values)
    {
        foreach (var (time, _) in values)
            _duration = MathF.Max(time, _duration);
        
        _lines.Add(new AnimationLine<TValue>(values, setter, AnimationFuncs.GetFunc<TValue>()));
        return this;
    }

    public Animation Result()
    {
        var animation = new Animation(_duration, _lines.ToArray());
        _lines.Clear();
        _duration = 0;
        return animation;
    }
}