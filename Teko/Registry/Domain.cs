using System.Collections.ObjectModel;
using Teko.Resources;

namespace Teko.Registry;

public class Domain<T> : IDomain where T : class
{
    private Dictionary<string, T> _resources = new();
    
    public bool Load(ResourcesLoader loader, string file, string prefix)
    {
        var resource = loader.LoadResource<T>(file);
        if (resource == null) 
            return false;
        _resources.Add(prefix + Path.GetFileName(file).Split(".")[0], resource);
        return true;
    }

    public void Add(string name, T resource)
        => _resources[name] = resource;

    public T? Get(string name)
    {
        _resources.TryGetValue(name, out var result);
        return result;
    }

    public ReadOnlyDictionary<string, T> GetAll()
        => _resources.AsReadOnly();
}