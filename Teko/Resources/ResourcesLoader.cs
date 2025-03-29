using System.Reflection;
using SFML.Audio;
using SFML.Graphics;
using Teko.Core;
using Teko.Inject;

namespace Teko.Resources;

public class ResourcesLoader(string[] paths) : AService
{
    private Dictionary<Type, IResourceImporter> _importers = new();
    private Dictionary<Type, Type> _associatedResources = new();
    
    private List<ResourcesPack> _packs = new();
    [Inject] private Logger? _logger;
    
    protected override void OnSetup()
    {
        AddImporter(new SfmlResourcesImporter());
        
        AddAssociatedResource<SfmlResourcesImporter, Texture>();
        AddAssociatedResource<SfmlResourcesImporter, Font>();
        AddAssociatedResource<SfmlResourcesImporter, Music>();
        AddAssociatedResource<SfmlResourcesImporter, Sound>();
        
        Reload();
    }

    public void Reload()
    {
        foreach (var path in paths)
        {
            foreach (var root in Directory.GetDirectories(path))
            {
                try
                {
                    var pack = new ResourcesPack(root);
                    _packs.Add(pack);
                    _logger?.Info($"Resources: Detected new pack \"{pack.Name}\" at \"{root}\"");
                }
                catch (Exception exception)
                {
                    _logger?.Error($"Resources: Invalid pack at \"{root}\": {exception.Message}");
                }
            }
        }
        
        _packs.Sort((aPack, bPack) => aPack.Priority - bPack.Priority);
    }

    public TResource? LoadResource<TResource>(string path) where TResource : class
    {
        var importer = GetResourceImporter<TResource>();
        
        foreach (var pack in _packs)
        {
            var stream = pack.GetFile(path);

            if (stream == null)
            {
                _logger?.Info($"Resources: Pack \"{pack.Name}\" don't have resource \"{path}\"");
                continue;
            }

            try
            {
                return importer.ImportResource<TResource>(stream);
            }
            catch (Exception exception)
            {
                _logger?.Error($"Resources: Failed load resource \"{path}\" from pack \"{pack.Name}\": {exception.Message}");
            }
        }

        _logger?.Error($"Resources: Failed load resource \"{path}\", none of packs contain a valid resource");
        
        return default;
    }

    public string[] ListFilesAt(string path)
    {
        var result = new List<string>();

        foreach (var pack in _packs)
            result.AddRange(pack.ListFilesAt(path));

        return result.ToArray();
    }

    public string[] ListDirsAt(string path)
    {
        var result = new List<string>();

        foreach (var pack in _packs)
            result.AddRange(pack.ListDirsAt(path));

        return result.ToArray();
    }

    public void AddImporter<TImporter>(TImporter importer) where TImporter : IResourceImporter
        => _importers[typeof(TImporter)] = importer;

    public void AddAssociatedResource<TImporter, TResource>() where TImporter : IResourceImporter
        => _associatedResources[typeof(TResource)] = typeof(TImporter);
    
    
    public IResourceImporter GetResourceImporter<TResource>()
    {
        var type = typeof(TResource);
        
        if (_associatedResources.TryGetValue(type, out var importerType))
            return _importers[importerType];

        Type? importer = null;
        
        if (type.IsAssignableTo(typeof(IKnownImporter)))
            importer = (Type?)type.InvokeMember(nameof(IKnownImporter.GetImporterType), BindingFlags.Static, null, null, [])!;

        if (importer == null)
            throw new Exception($"A resource '{type.Name}' not have associated importer.");

        _associatedResources[type] = importer;

        return _importers[importer];
    }
}