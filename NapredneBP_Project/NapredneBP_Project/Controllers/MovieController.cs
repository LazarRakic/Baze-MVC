using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NapredneBP_Project.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly IGraphClient _client;
        private readonly RedisService _redisService;

        public MovieController(IGraphClient client, RedisService redisService)
        {
            _client = client;
            _redisService = redisService;
        }

        public async Task<IActionResult> Index()
        {
            return View("CreateMovie");
        }

        public async Task<IActionResult> GMBT()
        {
            return View("GetMovieByTitle");
        }

        [HttpPost]
        [Route("CreateMovie")]
        public async Task<IActionResult> CreateMovie(MovieDTO movie)
        {

            Movie movieNew = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = movie.Title,
                Description = movie.Description,
                ImageUri = movie.ImageUri,
                PublishingDate = movie.PublishingDate,
                Rate = 0,
                RateCount = 0,
                RateSum = 0
            };

            await _client.Cypher.Create("(m:Movie $movie)")
                                .WithParam("movie", movieNew)
                                .ExecuteWithoutResultsAsync();

            var labele = movie.Labels.First().Split(", ");
            foreach (var obj in labele)
            {
                await _client.Cypher.Match("(m:Movie)")
                                .Where((Movie m) => m.Title == movie.Title)
                                .Set("m:" + obj + "")
                                .ReturnDistinct(m => m.Labels()).ResultsAsync;
            }

            return RedirectToAction("AllMovies");
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
            var movieRedis = _redisService.Get(id.ToString());

            if (movieRedis.Result != null)
            {
                MovieDTO m = JsonSerializer.Deserialize<MovieDTO>(movieRedis.Result);

                System.Diagnostics.Debug.WriteLine("DONE BY REDIS!!!");

                return View("Details", m);
            }
            else
            {
                MovieDTO a = new MovieDTO();

                var movie0 = await _client.Cypher.Match("(m:Movie)")
                                                 .Where((Movie m) => m.Id == id)
                                                 .Return((m) => new { film = m.As<Movie>() }).ResultsAsync;

                var movie1 = await _client.Cypher.Match("(m:Movie)<-[rel:Directed_by]-(p:Person)")
                                                 .Where((Movie m) => m.Id == id)
                                                 .Return((m, p) => new {
                                                      film = m.As<Movie>(),
                                                      directors = p.CollectAs<Person>()}).ResultsAsync;

                var movie2 = await _client.Cypher.Match("(m:Movie)<-[rel:Acted_in]-(p:Person)")
                                                 .Where((Movie m) => m.Id == id)
                                                 .Return((m, p) => new {
                                                      film = m.As<Movie>(),
                                                      actors = p.CollectAs<Person>()}).ResultsAsync;

                foreach (var item in movie0)
                {
                    a.Id = item.film.Id;
                    a.Title = item.film.Title;
                    a.Description = item.film.Description;
                    a.ImageUri = item.film.ImageUri;
                    a.PublishingDate = item.film.PublishingDate;
                    a.Rate = item.film.Rate;
                }

                foreach (var obj in movie1)
                {
                    a.ListOfDirectors = obj.directors;
                }

                foreach (var obj in movie2)
                {
                    a.ListOfActors = obj.actors;
                }


                var movieLabels = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Title == a.Title)
                                            .ReturnDistinct(m => m.Labels()).ResultsAsync;

                a.Labels = (IEnumerable<string>)movieLabels.First();

                await _redisService.Set(a.Id.ToString(), JsonSerializer.Serialize(a));

                return View("Details", a);
            }

            
        }

        [HttpGet]
        [Route("GetMovieByTitle")]
        public async Task<IActionResult> GetMovieByTitle(Movie movie)
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                             .Where((Movie m) => m.Title == movie.Title)
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

        [HttpGet]
        [Route("AddMovieRate/{id}")]
        public async Task<IActionResult> AddMovieRate(Guid id, MovieDTO movie)
        {
            if (movie.Rate >= 0 && movie.Rate <= 5)
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
                m.RateSum += movie.Rate;
                m.Rate = m.RateSum / m.RateCount;

                query = await _client.Cypher.Match("(m:Movie)")
                                                .Where((Movie m) => m.Id == id)
                                                .Set("m = $movie")
                                                .WithParam("movie", m)
                                                .Return((m) => new { movie = m.As<Movie>() }).ResultsAsync;

                return RedirectToAction("AllMovies");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("DeleteMovieById/{id}")]
        public async Task<IActionResult> DeleteMovieById(Guid id)
        {
            await _client.Cypher.Match("(m:Movie)")
                                .Where((Movie m) => m.Id == id)
                                .DetachDelete("m")
                                .ExecuteWithoutResultsAsync();

            return RedirectToAction("AllMovies");
        }

        /*[HttpGet]
        [Route("GetMovieByGenre")]
        public async Task<IActionResult> GetMovieByGenre(Movie movie)
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                             .Where((Movie m) => m.Genre == movie.Genre)
                                             .Return(m => m.As<Movie>()).ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;
            return View("AllMovies", ListofMovies);
        }*/


        public async Task<IEnumerable<string>> GetAllLabelsForMovie(string title)
        {
            var movieLabels = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Title == title)
                                            .ReturnDistinct(m => m.Labels()).ResultsAsync;

            IEnumerable<string> newList = (IEnumerable<string>)movieLabels;

            return newList;
        }

        [HttpGet]
        [Route("GetAllLabels")]
        public async Task<IActionResult> GetAllLabels()
        {
            var movieLabels = await _client.Cypher.Match("(m)")
                                            .ReturnDistinct(m => m.Labels()).ResultsAsync;
            return Ok(movieLabels);
        }

        [HttpPost]
        [Route("SetLabelForMovie/{title}/{label}")]
        public async Task<IActionResult> GetAllLabels(string title, string label)
        {
            var movieLabels = await _client.Cypher.Match("(m:Movie)")
                                                  .Where((Movie m) => m.Title == title)
                                                  .Set("m:" + label + "")
                                                  .ReturnDistinct(m => m.Labels()).ResultsAsync;
            return Ok(movieLabels);
        }

        //TREBA IZ REDISA
        /*
        [HttpGet]
        [Route("TopMovies")]
        public async Task<IActionResult> GetTopMovies()
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                            .Return(m => m.As<Movie>())
                                            .OrderBy("m.Rate")
                                            .Limit(5)
                                            .ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;
            return View(ListofMovies);
        }*/
    }
}
