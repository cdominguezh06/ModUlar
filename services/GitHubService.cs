using Microsoft.Extensions.Logging;

namespace ModUlar.services;

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
            string url = "https://raw.githubusercontent.com/cdominguezh06/ModUlar/b9748494e55d55a3984a4c8a653bd51a2c944bbb/README.md";
            
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