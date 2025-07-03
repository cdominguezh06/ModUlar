using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ModUlar.services;

public class GitHubService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GitHubService> _logger;

    private const string Owner = "cdominguezh06";
    private const string Repo  = "ModUlar";

    public GitHubService(HttpClient httpClient, ILogger<GitHubService> logger)
    {
        _httpClient = httpClient;
        _logger     = logger;
    }

    public async Task<string> GetReadmeContentAsync()
    {
        try
        {
            var sha = await GetLatestCommitShaAsync();
            if (string.IsNullOrWhiteSpace(sha))
            {
                _logger.LogError("No se pudo obtener el SHA del último commit.");
                return "No se pudo cargar el README.md";
            }

            var url = $"https://raw.githubusercontent.com/{Owner}/{Repo}/{sha}/README.md";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            _logger.LogError("Error al obtener el README: {StatusCode}", response.StatusCode);
            return "No se pudo cargar el README.md";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al intentar obtener el README de GitHub");
            return "Error al cargar el README.md";
        }
    }

    private async Task<string?> GetLatestCommitShaAsync()
    {
        var apiUrl = $"https://api.github.com/repos/{Owner}/{Repo}/commits?per_page=1";
        using var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

        // GitHub exige un User-Agent; añade otro encabezado si usas token.
        request.Headers.UserAgent.ParseAdd("ModUlar-App");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Error al obtener el último commit: {StatusCode}", response.StatusCode);
            return null;
        }

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var json = await JsonDocument.ParseAsync(stream);

        // Respuesta = array de commits; tomamos el primero.
        return json.RootElement[0].GetProperty("sha").GetString();
    }
}