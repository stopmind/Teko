using System.Collections.ObjectModel;
using Teko.Resources;

namespace Teko.Registry;

public class Domain<T> : IDomain where T : IResource
{
    private Dictionary<string, T> _resources = new();
    
    public void Load(ResourcesLoader loader, string file, string prefix)
        => _resources.Add(prefix + Path.GetFileName(file).Split(".")[0], loader.LoadResource<T>(file)!);

    public T? Get(string name)
    {
        _resources.TryGetValue(name, out var result);
        return result;
    }

    public ReadOnlyDictionary<string, T> GetAll()
        => _resources.AsReadOnly();
}