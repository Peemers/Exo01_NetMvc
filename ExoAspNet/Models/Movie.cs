using System.Text.Json.Serialization;

namespace ExoAspNet.Models;

public class Movie
{
  public int Id { get; set; }
  [JsonPropertyName("title")]
  public string Titre { get; set; } = string.Empty;
  public string Genre { get; set; } = string.Empty;
  public int Annee { get; set; }
}