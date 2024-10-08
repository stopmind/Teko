namespace Teko.Inject;

public interface ISource
{
    object? GetValue(Type type);
}