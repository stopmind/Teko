using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teko.Core;
using Teko.Resources;

namespace Teko.Registry;

public class LoadConfig : AJsonResource
{
    public readonly Dictionary<string, string[]> Paths;
    public readonly string Prefix;

    public LoadConfig(Dictionary<string, string[]> paths, string prefix)
    {
        Paths = paths;
        Prefix = prefix;
    }
}