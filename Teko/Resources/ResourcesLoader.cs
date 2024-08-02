using Teko.Core;

namespace Teko.Resources;

public class ResourcesLoader(string[] paths) : AService
{
    private List<ResourcesPack> _packs = new();
    
    protected override void OnSetup()
    {
        foreach (var path in paths)
            foreach (var root in Directory.GetDirectories(path))
            {
                try
                {
                    var pack = new ResourcesPack(root);
                    _packs.Add(pack);
                    Console.WriteLine($"Resources: Detected new pack \"{pack.Name}\" at \"{root}\"");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Resources: Invalid pack at \"{root}\": {exception.Message}");
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
                Console.WriteLine($"Resources: Pack \"{pack.Name}\" don't have resource \"{path}\"");
                continue;
            }

            try
            {
                return TResource.Load(stream);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Resources: Failed load resource \"{path}\" from pack \"{pack.Name}\": {exception.Message}");
            }
        }

        Console.WriteLine($"Resources: Failed load resource \"{path}\", none of packs contain a valid resource");
        
        return default;
    }
}