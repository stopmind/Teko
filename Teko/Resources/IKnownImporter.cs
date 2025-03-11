namespace Teko.Resources;

public interface IKnownImporter
{
    static abstract Type GetImporterType();
}