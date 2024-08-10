using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teko.Resources;

namespace Teko.Registry;

public class LoadConfig : IResource
{
    public readonly Dictionary<string, string[]> DomainsLoadPaths;
    public readonly string Prefix;
    
    public static dynamic Load(ResourcesLoader loader, Stream stream)
    {
        var reader = new StreamReader(stream);
        var data = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd())!;
        reader.Close();
        
        return new LoadConfig(
            data["Paths"]!.Value<Dictionary<string, string[]>>()!,
            data["Prefix"]!.Value<string>()!
        );
    }

    private LoadConfig(Dictionary<string, string[]> domainsLoadPaths, string prefix)
    {
        DomainsLoadPaths = domainsLoadPaths;
        Prefix = prefix;
    }
}