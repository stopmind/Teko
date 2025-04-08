namespace Teko.Resources;

public abstract class AResourceImporter
{
    public abstract TResource ImportResource<TResource>(Stream stream) where TResource : class;

    public virtual bool SaveStream<TResource>()
        => false;
}