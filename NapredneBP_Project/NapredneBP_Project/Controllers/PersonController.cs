using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NapredneBP_Project.Controllers
{
    public class PersonController : Controller
    {
        
        private readonly IGraphClient _client;
        private readonly RedisService _redisService;
        public PersonController(IGraphClient client, RedisService redisService)
        {
            _client = client;
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("CreatePerson")]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            Person person_new = new Person()
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                BornYear = person.BornYear
            };
            await _client.Cypher.Create("(p:Person $person)")
                                .WithParam("person", person_new)
                                .ExecuteWithoutResultsAsync();

            return RedirectToAction("GetAllPersons");
        }

        [HttpGet]
        [Route("GetAllPersons")]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _client.Cypher.Match("(n:Person)")
                                              .Return(n => n.As<Person>()).ResultsAsync;
            IEnumerable<Person> p = persons;
            return View(p);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var person = await _client.Cypher.Match("(n:Person)")
                                             .Where((Movie n) => n.Id == id)
                                             .Return(n => n.As<Person>()).ResultsAsync;
            Person per = person.First();
            return View(per);
        }

        [HttpPost]
        [Route("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson(Person per)
        {
            var result = await _client.Cypher.Match("(p:Person)")
                                                .Where((Person p) => p.Id == per.Id)
                                                .Return(n => n.As<Person>()).ResultsAsync;

            Person personOld = result.First();

            foreach(var obj in per.Relationships)
            {
                personOld.Relationships.Add(obj);
            }

            await _client.Cypher.Match("(p:Person)")
                                .Set("p=$person").WithParam("person", personOld)
                                .Where((Person p) => p.Id == per.Id).ExecuteWithoutResultsAsync();

            return RedirectToAction("GetAllPersons");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var person = await _client.Cypher.Match("(p:Person)")
                                            .Where((Person p) => p.Id == id)
                                            .Return(p => p.As<Person>()).ResultsAsync;
            Person per = person.First();
            return View(per);
        }

        [HttpPost]
        [Route("DeletePerson")]
        public async Task<IActionResult> DeletePerson(Person per)
        {
            await _client.Cypher.Match("(p:Person)")
                                .Where((Person p) => p.Id == per.Id)
                                .DetachDelete("p")
                                .ExecuteWithoutResultsAsync();

            return RedirectToAction("GetAllPersons");
        }

        [HttpGet]
        [Route("GetProperty/{id}")]
        public async Task<IActionResult> GetProperty(Guid id)
        {

            var person = await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Id == id).Return(p => p.As<Person>().Name).ResultsAsync;
            return Ok(person);
        }

        [HttpGet]
        [Route("GetPersonsById/{id}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            var person = await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Id == id).Return(p => p.As<Person>()).ResultsAsync;
            return Ok(person);
        }
        [HttpGet]
        [Route("GetPersonByName/{name}")]
        public async Task<IActionResult> GetPersonByName(String name)
        {
            var person = await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Name == name).Return(p => p.As<Person>()).ResultsAsync;
            return Ok(person);
        }
        [HttpDelete]
        [Route("DeletePersonByName/{name}")]
        public async Task<IActionResult> DeletePersonByName(string name)
        {
            await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Name == name).Delete("p").ExecuteWithoutResultsAsync();

            return Ok();
        }
        [HttpPut]
        [Route("UpdatePersonByName/{name}")]
        public async Task<IActionResult> UpdatePersonByName(string name, Person person)
        {
            Person person_new = new Person()
            {
                Name = person.Name,
                BornYear = person.BornYear,
            };

            await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Name == name).Set("p=$person").WithParam("person", person_new).ExecuteWithoutResultsAsync();
            return Ok();
        }

        public async Task<IActionResult> Connect()
        {
            return View();
        }

        [HttpGet]
        [Route("Relationship")]
        public async Task<IActionResult> RelationshipConnect(Models.Relationship relationship)
        {
            if (relationship.Name.ToLower() == "actedin")
                relationship.Name = "Acted_in";
            if (relationship.Name.ToLower() == "directedby")
                relationship.Name = "Directed_by";

            //Models.Relationship relationship1 = new Models.Relationship()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = relationship.Name,
            //    PersonName = relationship.PersonName,
            //    MovieName = relationship.MovieName
            //};

            var rel = await _client.Cypher.Match("(p:Person), (m:Movie)")
                                .Where((Person p, Movie m) => p.Name == relationship.PersonName && m.Title == relationship.MovieName)
                                .Create("(p)-[r:" + relationship.Name + "]->(m)")
                                //.WithParam("relation", relationship1)
                                .Return(m => m.As<Movie>())
                                .ResultsAsync;
            var result = _redisService.Get(rel.First().Id.ToString());
            if(result.Result != null)
            {
                var movie0 = await _client.Cypher.Match("(m:Movie)")
                                                 .Where((Movie m) => m.Title == relationship.MovieName)
                                                 .Return((m) => new { film = m.As<Movie>() }).ResultsAsync;

                var movie1 = await _client.Cypher.Match("(m:Movie)<-[rel:Directed_by]-(p:Person)")
                                                 .Where((Movie m) => m.Title == relationship.MovieName)
                                                 .Return((m, p) => new {
                                                     film = m.As<Movie>(),
                                                     directors = p.CollectAs<Person>()
                                                 }).ResultsAsync;

                var movie2 = await _client.Cypher.Match("(m:Movie)<-[rel:Acted_in]-(p:Person)")
                                                 .Where((Movie m) => m.Title == relationship.MovieName)
                                                 .Return((m, p) => new {
                                                     film = m.As<Movie>(),
                                                     actors = p.CollectAs<Person>()
                                                 }).ResultsAsync;

                MovieDTO a = new MovieDTO();

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
            }
            
            return RedirectToAction("GetAllPersons");
        }

        [HttpGet]
        [Route("{rtype}/Diconnect/{title}")]
        public async Task<IActionResult> Disconnect(string rtype, string title)
        {
            if (rtype.ToLower() == "actedin")
                rtype = "Acted_in";
            if (rtype.ToLower() == "directedby")
                rtype = "Directed_by";

            await _client.Cypher.Match("p=()-[r:" + rtype + "]->(m:Movie)")
                                .Where((Movie m) => m.Title == title)
                                .Delete("r")
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }
    }
}
