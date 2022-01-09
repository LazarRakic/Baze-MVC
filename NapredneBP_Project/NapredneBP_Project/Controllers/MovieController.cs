using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly IGraphClient _client;
        //ovde vrati sve filmove
        

        public MovieController(IGraphClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            return View("CreateMovie");
        }

        [HttpPost]
        [Route("CreateMovie")]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {

            Movie movieNew = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = movie.Title,
                Description = movie.Description,
                ImageUri = movie.ImageUri,
                PublishingDate = movie.PublishingDate,
                Rate = 0,
                RateCount = 0
            };

            await _client.Cypher.Create("(m:Movie $movie)")
                                .WithParam("movie", movieNew)
                                .ExecuteWithoutResultsAsync();

            var movies = await _client.Cypher.Match("(m:Movie)")
                                            .Return(m => m.As<Movie>()).ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;

            return View("AllMovies", ListofMovies);
        }

        [HttpGet]
        [Route("AllMovies")]
        public async Task<IActionResult> AllMovies()
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                            .Return(m => m.As<Movie>()).ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;
            return View(ListofMovies);
        }

        [HttpGet]
        [Route("GetMovieById/{id}")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            var movie = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Id == id)
                                            .Return((m) => new { film = m.As<Movie>() }).ResultsAsync;
            Movie a = new Movie();
            foreach (var item in movie)
            {
                a = item.film;
            }
            return View(a);
        }

        [HttpGet]
        [Route("GetMovieByTitle/{title}")]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                             .Where((Movie m) => m.Title == title)
                                             .Return(m => m.As<Movie>()).ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;
            return View("AllMovies", ListofMovies);
        }

        [HttpGet]
        [Route("GetMovieByRate/{rate}")]
        public async Task<IActionResult> GetMovieByRate(double rate)
        {
            var movie = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Rate == rate)
                                            .Return(m => m.As<Movie>()).ResultsAsync;
            return Ok(movie);
        }

        [HttpPut]
        [Route("AddMovieRate/{id}/{rate}")]
        public async Task<IActionResult> AddMovieRate(Guid id, double rate)
        {
            if (rate >= 0 && rate <= 5)
            {
                var query = await _client.Cypher.Match("(m:Movie)")
                                                .Where((Movie m) => m.Id == id)
                                                .Return((m) => new { movie = m.As<Movie>() }).ResultsAsync;

                Movie m = new Movie();
                foreach (var item in query)
                {
                    m = item.movie;
                }

                m.RateCount += 1;
                m.Rate += rate;
                m.Rate = m.Rate / m.RateCount;

                query = await _client.Cypher.Match("(m:Movie)")
                                                .Where((Movie m) => m.Id == id)
                                                .Set("m = $movie")
                                                .WithParam("movie", m)
                                                .Return((m) => new { movie = m.As<Movie>() }).ResultsAsync;
                return Ok(query);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteMovieById/{id}")]
        public async Task<IActionResult> DeleteMovieById(Guid id)
        {
            await _client.Cypher.Match("(m:Movie)")
                                .Where((Movie m) => m.Id == id)
                                .Delete("m")
                                .ExecuteWithoutResultsAsync();

            return Ok();
        }
    }
}
