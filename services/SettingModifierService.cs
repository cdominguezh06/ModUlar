using PacoYakuzaMAUI.model;

namespace PacoYakuzaMAUI.services;

public class SettingModifierService
{
    public void ModifySetting(Setting setting, String modFolder)
    {
        foreach (var file in setting.Files)
        {
            var path = (modFolder + "/" + file.Folder).Replace('\\', '/');
            foreach (var realFile in Directory.GetFiles(path))
            {
                var pathParts = realFile.Replace('\\', '/').Split('/');
                var fileName = pathParts.Last();
                if (fileName.Contains(file.Name))
                {
                    if (setting.Activo && fileName.StartsWith('.'))
                    {
                        File.Move(realFile, path + "/" + file.Name);
                    } else if (!setting.Activo && !fileName.StartsWith('.'))
                    {
                        File.Move(realFile, path + "/." + file.Name);
                    }
                }
            }
        }
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