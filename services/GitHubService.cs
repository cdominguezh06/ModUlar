namespace PacoYakuzaMAUI.services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
public class GitHubService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GitHubService> _logger;
    
    public GitHubService(HttpClient httpClient, ILogger<GitHubService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<string> GetReadmeContentAsync()
    {
        try
        {
            // URL para acceder al README.md en formato raw
            string url = "https://raw.githubusercontent.com/cdominguezh06/spylook/9f6ee7fffd0ab994eea03480b3940fa9294aea0b/README.md";
            
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                _logger.LogError($"Error al obtener el README: {response.StatusCode}");
                return "No se pudo cargar el README.md";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al intentar obtener el README de GitHub");
            return "Error al cargar el README.md";
        }
    }
}