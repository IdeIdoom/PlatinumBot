using PlatinumBot.Modules;

namespace PlatinumBot.Modules;

public class JSONLoader
{
    public string JsonData {get; private set;}
    public void Load(string path)
    {
        using StreamReader streamReader = new(path);
        JsonData = streamReader.ReadToEnd();
    }
}
