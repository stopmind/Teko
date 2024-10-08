using System.Reflection;

namespace Teko.Inject;

public class Injector
{
    private ISource[] _sources;

    public void Inject(object target)
    {
        var fields = target.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(field => field.CustomAttributes.Any(data => data.AttributeType == typeof(InjectAttribute)))
            .ToArray();

        foreach (var field in fields)
        {
            var injected = false;
            foreach (var source in _sources)
            {
                var value = source.GetValue(field.FieldType);

                if (value != null)
                {
                    injected = true;
                    field.SetValue(target, value);
                    break;
                }
            }
            
            if (!injected)
                throw new Exception($"Failed inject field {field.Name}");
        }
    }
    
    public Injector(ISource[] sources)
    {
        _sources = sources;
    }
}