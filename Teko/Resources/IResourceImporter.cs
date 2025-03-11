namespace Teko.Resources;

public interface IResourceImporter
{
    TResource ImportResource<TResource>(Stream stream) where TResource : class;
}