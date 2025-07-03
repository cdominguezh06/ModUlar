using ModUlar.model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ModUlar.services;

public class GameService
{
    private List<Game> Games { get; set; } = new List<Game>();
    public event Action GamesChanged;

    public List<Game> GetGamesWithMods()
    {
        if (Games.Count == 0)
        {
            LoadGames();
            if (Games.Count == 0)
            {
                return Games;
            }

            Games.ForEach(game => { AddMods(game); });
        }

        return Games;
    }

    private void AddMods(Game game)
    {
        game.Mods = new List<Mod>();
        Directory.GetDirectories((game.GameFolder + "/mods").Replace('/', '\\')).ToList().ForEach(dir =>
        {
            var modYamlPath = Path.Combine(dir, "modular.yaml");
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
                            var path = Path.Combine(dir, file.Folder, file.Name);
                            if (!File.Exists(path))
                            {
                                active = false;
                            }
                        });
                        setting.Activo = active;
                    });
                    mod.Folder = dir;
                    game.Mods.Add(mod);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        });
    }

    private void LoadGames()
    {
        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();
        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml"))
        {
            var yamlContent = "Games: []\n";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml", yamlContent);
            return;
        }

        using var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml");
        var yaml = reader.ReadToEnd();
        var dict = deserializer.Deserialize<Dictionary<string, List<Game>>>(yaml);
        Games = dict.TryGetValue("Games", out var games) ? games : new List<Game>();
    }

    public string GetParentDirectory()
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

    public void AddGameToYaml(Game newGame)
    {
        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

        var serializer = new SerializerBuilder()
            .Build();

        // Leer juegos existentes
        List<Game> games = new();
        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml"))
        {
            using var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml");
            var dict = deserializer.Deserialize<Dictionary<string, List<Game>>>(reader);
            if (dict != null && dict.TryGetValue("Games", out var loadedGames) && loadedGames != null)
                games = loadedGames;
        }

        // Agregar el nuevo juego si no existe (por nombre y carpeta)
        if (!games.Any(g => g.Name == newGame.Name))
            games.Add(newGame);

        var outDict = new Dictionary<string, List<Game>> { { "Games", games } };
        var yaml = serializer.Serialize(outDict);
        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/games.yaml", yaml);
        games = games.Distinct().ToList();
        Games = games;
        Games.ForEach(AddMods);
        GamesChanged?.Invoke();
    }
    
}