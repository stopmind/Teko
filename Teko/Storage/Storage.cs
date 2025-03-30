using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Teko.Core;

namespace Teko.Storage;

public class Storage : AService
{
    private const string DataFileName = "Storage.data";

    private readonly string _root;
    
    private List<string> _cryptedFiles = new();
    private ICryptoTransform _encryptor;
    private ICryptoTransform _decryptor;

    public bool IsFileCrypted(string file)
        => file == DataFileName || _cryptedFiles.Contains(file);

    public void SetFileCrypted(string file, bool crypted)
    {
        if (crypted)
        {
            if (!IsFileCrypted(file))
                _cryptedFiles.Add(file);
            return;
        }

        _cryptedFiles.Remove(file);
    }

    public byte[] ReadFileBytes(string file)
    {
        var content = File.ReadAllBytes(Path.Combine(_root, file));

        if (IsFileCrypted(file))
            return _decryptor.TransformFinalBlock(content, 0, content.Length);

        return content;
    }

    public void WriteFileBytes(string file, byte[] content)
    {
        var path = Path.Combine(_root, file);
        
        if (IsFileCrypted(file))
            File.WriteAllBytes(path, _encryptor.TransformFinalBlock(content, 0, content.Length));
        else File.WriteAllBytes(path, content);
    }

    public string ReadFileText(string file)
        => Encoding.Unicode.GetString(ReadFileBytes(file));

    public void WriteFileText(string file, string content)
        => WriteFileBytes(file, Encoding.Unicode.GetBytes(content));

    public T? ReadFileJson<T>(string file)
        => JsonConvert.DeserializeObject<T>(ReadFileText(file));

    public void WriteFileJson(string file, object? value)
        => WriteFileText(file, JsonConvert.SerializeObject(value));
    
    public Storage(string root, SymmetricAlgorithm crypto, byte[] key, byte[] iv)
    {
        _root = root;

        _encryptor = crypto.CreateEncryptor(key, iv);
        _decryptor = crypto.CreateDecryptor(key, iv);

        Directory.CreateDirectory(root);
        
        var data = ReadFileJson<StorageData>(DataFileName);
        _cryptedFiles = data?.CryptedFiles ?? [];
    }

    protected override void OnSetup()
    {
        GameInner.ExitEvent += OnExit;
    }

    private void OnExit()
    {
        WriteFileJson(DataFileName, new StorageData(_cryptedFiles));
    }
}