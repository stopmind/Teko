using Teko.Core;
using Teko.Resources;

namespace Teko.Registry;

public class RegistryService : AService
{
    private Dictionary<string, IDomain> _domains = new();
    private ResourcesLoader? _loader;
    private Logger? _logger;
    
    protected override void OnSetup()
    {
        _loader = Game.GetService<ResourcesLoader>();
        _logger = Game.GetService<Logger>();
    }

    public void Load(LoadConfig config)
    {
        foreach (var (domainName, paths) in config.Paths)
        {
            _domains.TryGetValue(domainName, out var domain);
            if (domain == null)
            {
                _logger!.Warning($"Registry: Failed load unknown domain \"{domainName}\"");
                continue;
            }

            var count = 0;
            
            foreach (var path in paths)
            {
                var files = _loader!.ListFilesAt(path);
                count += files.Length;
                foreach (var file in files)
                    domain.Load(_loader!, Path.Combine(path, file), config.Prefix);
            }
            
            _logger!.Info($"Registry: Loaded new resources for domain \"{domainName}\" ({count})");
        }
    }

    public Domain<T> NewDomain<T>(string name) where T : IResource
    {
        var domain = new Domain<T>();
        _domains.Add(name, domain);
        return domain;
    }
    
    public Domain<T>? GetDomain<T>(string name) where T : IResource
    {
        _domains.TryGetValue(name, out var result);
        return (Domain<T>?)result;
    }
}