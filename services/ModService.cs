using ModUlar.model;

namespace ModUlar.services;

public static class ModService
{
    private static List<Mod> Mods { get; set; } = new List<Mod>();

    public static List<Mod> GetMods()
    {
        if (Mods.Count == 0)
        {
            foreach (var modDir in Directory.GetDirectories(GetParentDirectory()))
            {
                var modYamlPath = Path.Combine(modDir, "modular.yaml");
                if (File.Exists(modYamlPath))
                {
                    var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                        .IgnoreUnmatchedProperties()
                        .Build();
                    try
                    {
                        var mod = deserializer.Deserialize<Mod>(File.ReadAllText(modYamlPath));
                        mod.Settings.ForEach(setting =>
                        {
                            var active = true;
                            setting.Files.ForEach(file =>
                            {
                                var path = Path.Combine(modDir, file.Folder, file.Name);
                                if (!File.Exists(path))
                                {
                                    active = false;
                                }
                            });
                            setting.Activo = active;
                        });
                        mod.Folder = modDir;
                        Mods.Add(mod);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        return Mods;
    }
    
    public static string GetParentDirectory()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"Directorio base: {baseDir}");
    
        // Si la ruta termina con un separador, elimínalo
        if (baseDir.EndsWith(Path.DirectorySeparatorChar) || baseDir.EndsWith(Path.AltDirectorySeparatorChar))
        {
            baseDir = baseDir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            Console.WriteLine($"Directorio base sin separador final: {baseDir}");
        }
    
        // Ahora obtén el directorio padre
        string parentDir = Path.GetDirectoryName(baseDir);
        Console.WriteLine($"Directorio padre: {parentDir}");
    
        return parentDir;
    }
}