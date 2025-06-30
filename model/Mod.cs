using YamlDotNet.Serialization;

namespace ModUlar.model;

public class Mod
{
    public string Name { get; set; }
    public string Author { get; set; }
    
    public string Icon { get; set; } = "none"; // Default icon, can be overridden by mod
    
    public string Background { get; set; } = "none"; // Default background, can be overridden by mod
    public string Description { get; set; }
    public string Version { get; set; } 
    [YamlMember(Alias = "Settings")]
    public List<Setting> Settings { get; set; }

    public String Folder = "";
    public Mod()
    {
        Settings = new List<Setting>();
    }
    public Mod(string name, string author, string description, string version)
    {
        Name = name;
        Author = author;
        Description = description;
        Version = version;
    }
}