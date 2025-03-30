namespace Teko.Animations;

public static class AnimationFuncs
{
    private static float LinearFloat(Dictionary<float, float> values, float time)
    {
        KeyValuePair<float, float>? start = null;
        KeyValuePair<float, float>? end = null;

        foreach (var value in values)
        {
            if (value.Key <= time && (start == null || start.Value.Key < value.Key))
            {
                start = value;
                continue;
            }
            
            if (value.Key >= time && (end == null || end.Value.Key > value.Key))
                end = value;
        }

        if (start == null)
            return 0;

        if (end == null)
            return start.Value.Value;

        return (time - start.Value.Key) / (end.Value.Key - start.Value.Key) * 
            (end.Value.Value - start.Value.Value) + start.Value.Value;
    }

    private static int LinearInt(Dictionary<float, int> values, float time)
    {
        KeyValuePair<float, int>? start = null;
        KeyValuePair<float, int>? end = null;

        foreach (var value in values)
        {
            if (value.Key <= time && (start == null || start.Value.Key < value.Key))
            {
                start = value;
                continue;
            }
            
            if (value.Key >= time && (end == null || end.Value.Key > value.Key))
                end = value;
        }

        if (start == null)
            return 0;

        if (end == null)
            return start.Value.Value;

        return (int)((time - start.Value.Key) / (end.Value.Key - start.Value.Key) *
            (end.Value.Value - start.Value.Value) + start.Value.Value);
    }

    private static T UniversalFunc<T>(Dictionary<float, T> values, float time)
    {
        KeyValuePair<float, T>? start = null;

        foreach (var value in values)
        {
            if (value.Key <= time && (start == null || start.Value.Key < value.Key))
                start = value;
        }

        return start == null ? default(T)! : start.Value.Value;
    }

    private static Dictionary<Type, dynamic> _specialFuncs = new()
    {
        [typeof(float)] = (AnimationFunc<float>)LinearFloat,
        [typeof(int)] = (AnimationFunc<int>)LinearInt,
    };

    public static AnimationFunc<TValue> GetFunc<TValue>()
    {
        _specialFuncs.TryGetValue(typeof(TValue), out var func);
        return func as AnimationFunc<TValue> ?? UniversalFunc;
    }
}