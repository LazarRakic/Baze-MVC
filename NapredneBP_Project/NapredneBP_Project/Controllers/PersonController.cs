using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Controllers
{
    public class PersonController : Controller
    {
        
        private readonly IGraphClient _client;

        public PersonController(IGraphClient client)
        {
            _client = client;
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
            var persons = await _client.Cypher.Match("(n:Person)").Return(n => n.As<Person>()).ResultsAsync;
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
            await _client.Cypher.Match("(p:Person)").Where((Person p) => p.Id == per.Id)
                                .Set("p=$person")
                                .WithParam("person", per)
                                .ExecuteWithoutResultsAsync();
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
                                .Delete("p")
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
        //[Route("{pid}/{rtype}/{title}")]
        [Route("Relationship")]
        public async Task<IActionResult> RelationshipConnect(Models.Relationship relationship)
        {
            if (relationship.Name.ToLower() == "actedin")
                relationship.Name = "Acted_in";
            if (relationship.Name.ToLower() == "directedby")
                relationship.Name = "Directed_by";

            await _client.Cypher.Match("(p:Person), (m:Movie)")
                                .Where((Person p, Movie m) => p.Name == relationship.PersonName && m.Title == relationship.MovieName)
                                .Create("(p)-[r:" + relationship.Name + "]->(m)")
                                .ExecuteWithoutResultsAsync();
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
