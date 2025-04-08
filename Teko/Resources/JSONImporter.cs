﻿using Newtonsoft.Json;
using Teko.Core;

namespace Teko.Resources;

public class JsonImporter : AResourceImporter
{
    private JsonSubResourceConverter _converter = new(Game.GetService<ResourcesLoader>());
    
    public override TResource ImportResource<TResource>(Stream stream) where TResource : class
    {
        using (var reader = new StreamReader(stream))
        {
            return JsonConvert.DeserializeObject<TResource>(reader.ReadToEnd(), _converter)!;
        }
    }
}