namespace ModUlar.model;
public class FileItem
{
    public string Name { get; set; }
    public string Folder { get; set; }
}

public class Setting
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Activo { get; set; } = true;
    public List<FileItem> Files { get; set; }

    public Setting() {}

    public Setting(string name, List<FileItem> files, string description = "", bool activo = true)
    {
        this.Name = name;
        this.Files = files;
        this.Description = description;
        this.Activo = activo;
    }
}