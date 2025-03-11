using Teko.Core;

namespace Teko.Resources;

public interface IResource<TImporter>: IKnownImporter where TImporter: IResourceImporter
{
    new static Type GetImporterType()
        => typeof(TImporter);
}