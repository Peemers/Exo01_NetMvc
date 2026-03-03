using System.Text.Json;
using ExoAspNet.Models;

namespace ExoAspNet.Services;

public class TmdbService
{
  private readonly HttpClient _httpClient;
  private readonly IConfiguration _config;

  public TmdbService(HttpClient httpClient, IConfiguration config)
  {
    _httpClient = httpClient;
    _config = config;
  }

  public async Task<List<Movie>> GetPopularMoviesAsync()
  {
    var apiKey = _config["TMDB:apiKey"];
    var response = await _httpClient.GetAsync($"movie/popular?api_key={apiKey}&language=frFR");

    if (!response.IsSuccessStatusCode) return new List<Movie>();

    var content = await response.Content.ReadAsStringAsync();
    
    using var jsonDoc = JsonDocument.Parse(content);
    var resultJson = jsonDoc.RootElement.GetProperty("results").GetRawText();

    return JsonSerializer.Deserialize<List<Movie>>(resultJson) ?? new List<Movie>();
  }
  
}