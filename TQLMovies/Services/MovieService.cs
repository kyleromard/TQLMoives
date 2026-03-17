using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using TQLMovies.Models;

namespace TQLMovies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IStringLocalizer _language;

        public MovieService(IMovieRepository repository, IStringLocalizer language)
        {
            _repository = repository;
            _language = language;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _repository
                .GetAll()
                .Select(movie =>
                {
                    movie.Category = _language[movie.Category].Value;
                    return movie;
                })
                .ToList();
        }

        public Movie? GetById(int id)
        {
            var movie = _repository.GetById(id);
            if (movie == null)
            {
                return null;
            }

            movie.Category = _language[movie.Category].Value;
            return movie;
        }

        public Movie Add(Movie movie)
        {
            var created = _repository.Add(movie);

            created.Category = _language[created.Category].Value;

            return created;
        }
    }
}

