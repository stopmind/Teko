using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teko.Resources;

namespace Teko.Registry;

public class LoadConfig : IResource
{
    public readonly Dictionary<string, string[]> Paths;
    public readonly string Prefix;
    
    public static dynamic Load(ResourcesLoader loader, Stream stream)
    {
        var reader = new StreamReader(stream);
        var text = reader.ReadToEnd();
        reader.Close();

        return JsonConvert.DeserializeObject<LoadConfig>(text)!;
    }

    public LoadConfig(Dictionary<string, string[]> paths, string prefix)
    {
        Paths = paths;
        Prefix = prefix;
    }
}