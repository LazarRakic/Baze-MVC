using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

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

        public async Task<IActionResult> Index1()
        {
            return View();
        }

        public async Task<IActionResult> GMBT()
        {
            return View("GetMovieByTitle");
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
                RateCount = 0,
                RateSum = 0
            };

            await _client.Cypher.Create("(m:Movie $movie)")
                                .WithParam("movie", movieNew)
                                .ExecuteWithoutResultsAsync();

            return RedirectToAction("AllMovies");
        }

        [HttpGet]
        [Route("AllMovies")]
        public async Task<IActionResult> AllMovies()
        {
            var movies = await _client.Cypher.Match("(m:Movie)")
                                            .Return(m => m.As<Movie>()).ResultsAsync;
            IEnumerable<Movie> ListofMovies = movies;
            _redisService.Set("fsf", "tea");
            _redisService.Get("fsf");
            return View(ListofMovies);
        }

        [HttpGet]
        [Route("GetMovieById/{id}")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            /*var movie = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Id == id)
                                            .Return((m) => new { 
                                                film = m.As<Movie>()
                                                
                                            }).ResultsAsync;*/

           /* var movie1 = await _client.Cypher.Match("(m:Movie)-[rel:Acted_in]-(p:Person)")
                                             .Where((Movie m)=> m.Id==id)
                                             .Return((m,p) => new { 
                                                 film=m.As<Movie>(),
                                                 actors=p.CollectAs<Person>()
                                             }
                                             ).ResultsAsync;*/


            var movie = await _client.Cypher.Match("(director:Person)-[rel:Directed_by]->(m:Movie)<-[rela:Acted_in]-(actor:Person)")
                                             .Where((Movie m) => m.Id == id)
                                             .Return((m, actor, director) => new {
                                                 film = m.As<Movie>(),
                                                 actors = actor.CollectAs<Person>(),
                                                 directors = director.CollectAs<Person>()
                                             }
                                             ).ResultsAsync;

            
            Movie a = new Movie();
            foreach (var item in movie)
            {

                    a = item.film;
                if (item.actors != null && item.directors != null)
                {
                    a.ListOfActors = item.actors;
                    a.ListOfDirectors = item.directors;
                }

            }

            if (a.ListOfActors==null )
            {
                var movie1 = await _client.Cypher.Match("(m:Movie)-[rel:Directed_by]-(p:Person)")
                                              .Where((Movie m) => m.Id == id)
                                              .Return((m, p) => new {
                                                  film = m.As<Movie>(),
                                                  directros = p.CollectAs<Person>()
                                              }
                                              ).ResultsAsync;


                foreach (var item in movie1)
                {
                    a = item.film;
                    a.ListOfDirectors = item.directros;
                }

            }
            if (a.ListOfDirectors == null)
            {
                var movie2 = await _client.Cypher.Match("(m:Movie)-[rel:Acted_in]-(p:Person)")
                                              .Where((Movie m) => m.Id == id)
                                              .Return((m, p) => new {
                                                  film = m.As<Movie>(),
                                                  actors = p.CollectAs<Person>()
                                              }
                                              ).ResultsAsync;


                foreach (var item in movie2)
                {
                    a = item.film;
                    a.ListOfActors = item.actors;
                }

            }
            if(a.ListOfActors == null && a.ListOfDirectors == null)
            {
                var moviee = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Id == id)
                                            .Return((m) => new {
                                                film = m.As<Movie>()

                                            }).ResultsAsync;

                foreach (var item in moviee)
                {
                    a = item.film;
                    
                }
            }

            return View("Details", a);
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
        public async Task<IActionResult> AddMovieRate(Guid id,Movie movie)
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
                //return Ok(query);
                //return View("Index");
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

        [HttpGet]
        [Route("GetAllLabelsForMovie/{title}")]
        public async Task<IActionResult> GetAllLabelsForMovie(string title)
        {
            var movieLabels = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Title == title)
                                            .ReturnDistinct(m => m.Labels()).ResultsAsync;
            return Ok(movieLabels);
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
