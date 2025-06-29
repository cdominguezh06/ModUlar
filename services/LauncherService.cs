using Microsoft.Maui.ApplicationModel;

namespace PacoYakuzaMAUI.Services
{
    public class LauncherService : ILauncherService
    {
        public async Task<bool> OpenAsync(Uri uri)
        {
            try
            {
                return await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo abrir la URL: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> OpenAsync(string uri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out var parsedUri))
            {
                return await OpenAsync(parsedUri);
            }
            
            Console.WriteLine($"URL no válida: {uri}");
            return false;
        }
    }
}