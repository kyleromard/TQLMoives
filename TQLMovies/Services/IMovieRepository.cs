using System.Collections.Generic;
using TQLMovies.Models;

namespace TQLMovies.Services
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie? GetById(int id);
        Movie Add(Movie movie);
    }
}

