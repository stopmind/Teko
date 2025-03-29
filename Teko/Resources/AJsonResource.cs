namespace Teko.Resources;

public abstract class AJsonResource : IKnownImporter
{
    public static Type GetImporterType()
        => typeof(JsonImporter);
}