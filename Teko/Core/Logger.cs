namespace Teko.Core;

public class Logger(string path) : AService
{
    
    public event Action<LogType, string>? OnLog; 
    
    protected override void OnSetup()
    {
        var file = File.CreateText(path);
        OnLog += (type, text) =>
        {
            var typeLabel = "";

            switch (type)
            {
                case LogType.Error:
                    typeLabel = "Error";
                    break;
                case LogType.Info:
                    typeLabel = "Info ";
                    break;
                case LogType.Warning:
                    typeLabel = "Warn ";
                    break;
            }

            file.WriteLine($"[{typeLabel}] {text}");
        };

        GameInner.ExitEvent += () => file.Close();
    }

    private void Log(LogType type, string message)
        => OnLog?.Invoke(type, message);

    public void Info(string message)
        => Log(LogType.Info, message);
    
    public void Warning(string message)
        => Log(LogType.Warning, message);
    
    public void Error(string message)
        => Log(LogType.Error, message);
}