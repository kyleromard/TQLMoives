using Microsoft.AspNetCore.Mvc;
using TQLMovies.Models;
using TQLMovies.Services;

namespace TQLMovies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            var movies = _movieService.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Movie> Get(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            var created = _movieService.Add(movie);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}

