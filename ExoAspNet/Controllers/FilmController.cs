using ExoAspNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExoAspNet.Controllers;

public class FilmController : Controller
{
  private static readonly List<Movie> _movies = new()
  {
    new Movie { Id = 1, Titre = "Fight Club", Genre = "Drame", Annee = 1999 },
    new Movie { Id = 2, Titre = "Pulp Fiction", Genre = "Gangster", Annee = 1994 },
    new Movie { Id = 3, Titre = "Interstellar", Genre = "ScienceFiction", Annee = 2014 },
    new Movie { Id = 4, Titre = "2001:L'Odyssée de l'espace", Genre = "ScienceFiction", Annee = 1968 },
    new Movie { Id = 5, Titre = "Le Parrain", Genre = "Gangster", Annee = 1972 },
    new Movie { Id = 6, Titre = "Blade Runner", Genre = "ScienceFiction", Annee = 1982 },
    new Movie { Id = 7, Titre = "The Matrix", Genre = "ScienceFiction", Annee = 1999 },
    new Movie { Id = 8, Titre = "Inception", Genre = "ScienceFiction", Annee = 2010 },
    new Movie { Id = 9, Titre = "Gladiator", Genre = "Action", Annee = 2000 },
    new Movie { Id = 10, Titre = "The Dark Knight", Genre = "Action", Annee = 2008 },
    new Movie { Id = 11, Titre = "Se7en", Genre = "Thriller", Annee = 1995 },
    new Movie { Id = 12, Titre = "The Silence of the Lambs", Genre = "Thriller", Annee = 1991 },
    new Movie { Id = 13, Titre = "Forrest Gump", Genre = "Drame", Annee = 1994 },
    new Movie { Id = 14, Titre = "The Shawshank Redemption", Genre = "Drame", Annee = 1994 },
    new Movie { Id = 15, Titre = "The Prestige", Genre = "Mystere", Annee = 2006 },
    new Movie { Id = 16, Titre = "Memento", Genre = "Thriller", Annee = 2000 },
    new Movie { Id = 17, Titre = "Django Unchained", Genre = "Western", Annee = 2012 },
    new Movie { Id = 18, Titre = "The Departed", Genre = "Gangster", Annee = 2006 },
    new Movie { Id = 19, Titre = "Whiplash", Genre = "Drame", Annee = 2014 },
    new Movie { Id = 20, Titre = "The Green Mile", Genre = "Drame", Annee = 1999 },
    new Movie { Id = 21, Titre = "Parasite", Genre = "Thriller", Annee = 2019 },
    new Movie { Id = 22, Titre = "Joker", Genre = "Drame", Annee = 2019 },
    new Movie { Id = 23, Titre = "Mad Max: Fury Road", Genre = "Action", Annee = 2015 },
    new Movie { Id = 24, Titre = "Jurassic Park", Genre = "Aventure", Annee = 1993 },
    new Movie { Id = 25, Titre = "Titanic", Genre = "Romance", Annee = 1997 },
    new Movie { Id = 26, Titre = "Avatar", Genre = "ScienceFiction", Annee = 2009 },
    new Movie { Id = 27, Titre = "The Wolf of Wall Street", Genre = "Biopic", Annee = 2013 },
    new Movie { Id = 28, Titre = "Inglourious Basterds", Genre = "Guerre", Annee = 2009 },
    new Movie { Id = 29, Titre = "Shutter Island", Genre = "Thriller", Annee = 2010 },
    new Movie { Id = 30, Titre = "The Truman Show", Genre = "Drame", Annee = 1998 },
    new Movie { Id = 31, Titre = "The Social Network", Genre = "Drame", Annee = 2010 },
    new Movie { Id = 32, Titre = "Saving Private Ryan", Genre = "Guerre", Annee = 1998 },
    new Movie { Id = 33, Titre = "The Lion King", Genre = "Animation", Annee = 1994 },
    new Movie { Id = 34, Titre = "Back to the Future", Genre = "ScienceFiction", Annee = 1985 },
    new Movie { Id = 35, Titre = "The Grand Budapest Hotel", Genre = "Comedie", Annee = 2014 },
    new Movie { Id = 36, Titre = "No Country for Old Men", Genre = "Thriller", Annee = 2007 }
  };

  public IActionResult Index(string? genre = null, string? searchString = null, bool sort = false, bool sort2 = false, bool sort3 = false)
  {
    IEnumerable<ExoAspNet.Models.Movie> movies = _movies;
    
    if (!string.IsNullOrEmpty(genre))
    {
      movies = _movies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrEmpty(searchString))
    {
      movies = movies.Where(m => m.Titre.Contains(searchString, StringComparison.OrdinalIgnoreCase));
    }

    if (sort)
    {
      movies = movies.OrderBy(m => m.Titre);
    }

    if (sort2)
    {
      movies = movies.OrderBy(m => m.Annee);
    }

    if (sort3)
    {
      movies = movies.OrderBy(m => m.Genre);
    }
    
    ViewData["CurrentFilter"] = searchString;
    
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

      TempData["Success"] = $"Le Film {movie.Titre} a été supprimé avec succes.";
    }
    return RedirectToAction(nameof(Index));
  }
}