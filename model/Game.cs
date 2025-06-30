using YamlDotNet.Serialization;

namespace ModUlar.model;

public class Game
{
    public string Name { get; set; }
    [YamlIgnore]
    public List<Mod> Mods { get; set; }
    
    public string GameFolder { get; set; }

    public Game() { }
    
    public Game(string name, string gameFolder)
    {
        Name = name;
        GameFolder = gameFolder;
        Mods = new List<Mod>();
    }
    
}