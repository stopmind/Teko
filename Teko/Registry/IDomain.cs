using Teko.Resources;

namespace Teko.Registry;

public interface IDomain
{
    void Load(ResourcesLoader loader, string file, string prefix);
}