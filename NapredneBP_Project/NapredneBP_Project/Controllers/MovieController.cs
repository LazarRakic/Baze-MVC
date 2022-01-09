﻿using Microsoft.AspNetCore.Mvc;
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
            return View(ListofMovies);
        }

        [HttpGet]
        [Route("GetMovieById/{id}")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            /*var movie = await _client.Cypher.Match("(m:Movie)")
                                            .Where((Movie m) => m.Id == id)
                                            .Return((m) => new { 
                                                film = m.As<Movie>(),
                                                actors=m.As<Person>()
                                            }).ResultsAsync;*/

            var movie1 = await _client.Cypher.Match("(m:Movie)-[rel:Acted_in]-(p:Person)")
                                             .Where((Movie m)=> m.Id==id)
                                             .Return((m,p) => new { 
                                                 film=m.As<Movie>(),
                                                 actors=p.CollectAs<Person>()
                                             }
                                             ).ResultsAsync;
            Movie a = new Movie();
            foreach (var item in movie1)
            {
                a = item.film;
                a.ListOfActors = item.actors;
               
            }
            return View("Details",a);
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
                                .Delete("m")
                                .ExecuteWithoutResultsAsync();

            return RedirectToAction("AllMovies");
        }
    }
}
