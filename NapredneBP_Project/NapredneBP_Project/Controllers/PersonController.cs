using Microsoft.AspNetCore.Mvc;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        
        private readonly IGraphClient _client;

        public IActionResult Index()
        {
            return View();
        }
        public PersonController(IGraphClient client)
        {
            _client = client;
        }

        [HttpPost]
        [Route("CreateActor")]
        public async Task<IActionResult> CreateNodeActor(Person person)
        {

            Person person_new = new Person()
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                BornYear = person.BornYear,
            };
            await _client.Cypher.Create("(p:Person $person)")
                                .WithParam("person", person_new)
                                .ExecuteWithoutResultsAsync();

            return Ok();
        }
        [HttpGet]
        [Route("GetAllPersons")]
        public async Task<IActionResult> GetAllPersons()
        {

            var persons = await _client.Cypher.Match("(n:Person)").Return(n => n.As<Person>()).ResultsAsync;
            return Ok(persons);
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
    }
}
