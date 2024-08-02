namespace Teko.Resources;

public interface IResource
{ 
    static abstract dynamic Load(Stream stream);
}