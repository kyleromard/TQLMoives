using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TQLMovies.Models;

namespace TQLMovies.Services
{
    public class MovieRepository : IMovieRepository
    {
        private const string FileName = "Data/movies.json";
        private readonly object _lock = new object();

        public IEnumerable<Movie> GetAll()
        {
            return ReadAll();
        }

        public Movie? GetById(int id)
        {
            return ReadAll().FirstOrDefault(m => m.Id == id);
        }

        public Movie Add(Movie movie)
        {
            lock (_lock)
            {
                var movies = ReadAll().ToList();

                var nextId = movies.Count == 0 ? 1 : movies.Max(m => m.Id) + 1;
                movie.Id = nextId;

                movies.Add(movie);
                WriteAll(movies);

                return movie;
            }
        }

        private List<Movie> ReadAll()
        {
            if (!File.Exists(FileName))
            {
                return new List<Movie>();
            }

            var json = File.ReadAllText(FileName);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Movie>();
            }

            var result = JsonSerializer.Deserialize<List<Movie>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new List<Movie>();
        }

        private void WriteAll(List<Movie> movies)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(movies, options);
            File.WriteAllText(FileName, json);
        }
    }
}

