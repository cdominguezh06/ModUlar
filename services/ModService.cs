using PacoYakuzaMAUI.model;

namespace PacoYakuzaMAUI.services;

public static class ModService
{
    private static List<Mod> Mods { get; set; } = new List<Mod>();

    public static List<Mod> GetMods()
    {
        if (Mods.Count == 0)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var modDir in Directory.GetDirectories(dir))
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
}