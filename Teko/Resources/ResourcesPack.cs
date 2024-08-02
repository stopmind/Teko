using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Teko.Resources;

internal class ResourcesPack
{
    public readonly string Root;
    public readonly string Name;
    public readonly int Priority;

    public Stream? GetFile(string path)
    {
        var fullPath = Path.Combine(Root, path);
        return File.Exists(fullPath) ? File.Open(fullPath, FileMode.Open) : null;
    }
    
    public ResourcesPack(string root)
    {
        Root = root;

        var manifest = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Root, "manifest.json")));

        Name = manifest!["Name"]!.Value<string>()!;
        Priority = manifest!["Priority"]!.Value<int>();
    }
}