using Teko.Inject;

namespace Teko.Core;

public class ServicesSource : ISource
{
    public object? GetValue(Type type)
        => Game.TryGetServiceByType(type);
}