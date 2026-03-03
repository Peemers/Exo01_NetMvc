using ExoAspNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExoAspNet.Controllers;

public class FilmController : Controller
{
  private static readonly List<Movie> _movies = new()
  {
    new Movie { Id = 1, Titre = "Fight Club", Genre = "Drame/Thriller", Annee = 1999 },
    new Movie { Id = 2, Titre = "Pulp Fiction", Genre = "Gangster/Policier", Annee = 1994 },
    new Movie { Id = 3, Titre = "Interstellar", Genre = "ScienceFiction/Drame", Annee = 2014 },
    new Movie { Id = 4, Titre = "2001:L'Odyssée de l'espace", Genre = "ScienceFiction/Aventure", Annee = 1968 },
    new Movie { Id = 5, Titre = "Le Parrain", Genre = "Gangster/Policier", Annee = 1972 },
    new Movie { Id = 6, Titre = "Blade Runner", Genre = "ScienceFiction/Thriller", Annee = 1982 },
  };

  public IActionResult Index(string? genre = null, bool sort = false)
  {
    IEnumerable<ExoAspNet.Models.Movie> movies = _movies;
    
    if (!string.IsNullOrEmpty(genre))
    {
      movies = _movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
    }

    if (sort)
    {
      movies = movies.OrderBy(m => m.Titre);
    }
    return View(movies.ToList());
  }

  public IActionResult Details(int id)
  {
    Movie movie = _movies.FirstOrDefault(m => m.Id == id)!;

    if (movie == null)
    {
      return NotFound();
    }

    ViewData["Message"] = "Détails du film selectionné";
    return View(movie);
  }

  public IActionResult APropos()
  {
    ViewBag.TotalFilms = _movies.Count;
    TempData["Derniere Visite"] = DateTime.UtcNow.ToString("dd/MM/yyyy");
    return View();
  }

  [HttpGet]
  public IActionResult Supprimer(int id)
  {
    ExoAspNet.Models.Movie? movie = _movies.FirstOrDefault(m => m.Id == id);

    if (movie == null)
    {
      return NotFound();
    }
    return View(movie);
  }

  [HttpPost]
  [ActionName("Supprimer")]
  public IActionResult SupprimerConfirme(int id)
  {
    ExoAspNet.Models.Movie? movie = _movies.FirstOrDefault(m => m.Id == id);

    if (movie != null)
    {
      _movies.Remove(movie);

      TempData["Success"] = $"Le Film {movie.Titre} à été supprimer avec succes.";
    }
    return RedirectToAction(nameof(Index));
  }
}