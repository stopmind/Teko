using System.Text;
using Newtonsoft.Json;
using Teko.Core;

namespace Teko.Storage;

public class Storage : AService
{
    private readonly string _root;


    public byte[] ReadFileBytes(string file)
        => File.ReadAllBytes(Path.Combine(_root, file));

    public void WriteFileBytes(string file, byte[] content)
        => File.WriteAllBytes(Path.Combine(_root, file), content);

    public string ReadFileText(string file)
        => Encoding.Unicode.GetString(ReadFileBytes(file));

    public void WriteFileText(string file, string content)
        => WriteFileBytes(file, Encoding.Unicode.GetBytes(content));

    public T? ReadFileJson<T>(string file)
        => JsonConvert.DeserializeObject<T>(ReadFileText(file));

    public void WriteFileJson(string file, object? value)
        => WriteFileText(file, JsonConvert.SerializeObject(value));
    
    public Storage(string root)
    {
        _root = root;

        Directory.CreateDirectory(root);
    }

    protected override void OnSetup()
    {
        
    }

}