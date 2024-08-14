using Teko.Resources;

namespace Teko.Registry;

public interface IDomain
{
    bool Load(ResourcesLoader loader, string file, string prefix);
}