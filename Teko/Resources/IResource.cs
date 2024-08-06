namespace Teko.Resources;

public interface IResource
{ 
    static abstract dynamic Load(ResourcesLoader loader, Stream stream);
}