using Teko.Core;

namespace Teko.Resources;

public interface IResource
{ 
    static abstract dynamic Load(Game game, Stream stream);
}