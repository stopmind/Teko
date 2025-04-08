using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Teko.Core;
using Teko.DataStorage;

namespace Teko.Storage;

public class Storage : AService
{
    private const string StorageInfoFileName = ".info";
    
    private readonly string _root;
    private readonly List<string> _cryptedFiles = [];

    private readonly ICryptoTransform _decrytor;
    private readonly ICryptoTransform _encryptor;

    private static byte[] TransformData (byte[] bytes, ICryptoTransform transform) {
        using var memory = new MemoryStream();
        using var crypto = new CryptoStream(memory, transform, CryptoStreamMode.Write);
        crypto.Write(bytes, 0, bytes.Length);
        crypto.FlushFinalBlock();
        return memory.ToArray();
    }
    
    public bool IsFileCrypted(string file)
        => file == StorageInfoFileName || _cryptedFiles.Contains(file);

    public void SetFileCrypted(string file, bool crypted)
    {
        if (crypted)
        {
            if (!_cryptedFiles.Contains(file))
                _cryptedFiles.Add(file);
        }
        else _cryptedFiles.Remove(file);
    }

    public byte[]? ReadFileBytes(string file)
    {
        var path = Path.Combine(_root, file);
        if (!File.Exists(path))
            return null;
        
        var bytes = File.ReadAllBytes(path);

        if (IsFileCrypted(file))
            return TransformData(bytes, _decrytor);
            
        return bytes;
    }

    public void WriteFileBytes(string file, byte[] content)
    {
        if (IsFileCrypted(file))
            content = TransformData(content, _encryptor);
        
        File.WriteAllBytes(Path.Combine(_root, file), content);
    }

    public string? ReadFileText(string file)
    {
        var bytes = ReadFileBytes(file);

        if (bytes == null)
            return null;

        return Encoding.UTF8.GetString(bytes);
    }

    public void WriteFileText(string file, string content)
        => WriteFileBytes(file, Encoding.UTF8.GetBytes(content));

    public T? ReadFileJson<T>(string file)
    {
        var text = ReadFileText(file);

        if (text == null)
            return default;

        return JsonConvert.DeserializeObject<T>(text);
    }

    public void WriteFileJson(string file, object? value)
        => WriteFileText(file, JsonConvert.SerializeObject(value));
    
    public Storage(string root, string key)
    {
        _root = root;

        Directory.CreateDirectory(root);

        var bytes = Encoding.UTF8.GetBytes(key);
        Array.Resize(ref bytes, 16);
        
        var aes = Aes.Create();
        aes.Key = bytes;
        aes.IV = bytes;

        _decrytor = aes.CreateDecryptor();
        _encryptor = aes.CreateEncryptor();

        var info = ReadFileJson<StorageInfo>(StorageInfoFileName);
        
        if (info == null)
            return;
        
        _cryptedFiles = info.CryptedFiles;
    }

    protected override void OnSetup()
    {
        GameInner.ExitEvent += OnExit;
    }

    private void OnExit()
    {
        WriteFileJson(StorageInfoFileName, new StorageInfo()
        {
            CryptedFiles = _cryptedFiles
        });
    }
}