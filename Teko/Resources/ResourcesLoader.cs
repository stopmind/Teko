﻿using Teko.Core;

namespace Teko.Resources;

public class ResourcesLoader(string[] paths) : AService
{
    private List<ResourcesPack> _packs = new();
    private Logger? _logger;
    
    protected override void OnSetup()
    {
        _logger = Game.GetService<Logger>();
        
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
    }

    public TResource? LoadResource<TResource>(string path) where TResource : IResource
    {
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
                return TResource.Load(stream);
            }
            catch (Exception exception)
            {
                _logger?.Error($"Resources: Failed load resource \"{path}\" from pack \"{pack.Name}\": {exception.Message}");
            }
        }

        _logger?.Error($"Resources: Failed load resource \"{path}\", none of packs contain a valid resource");
        
        return default;
    }
}