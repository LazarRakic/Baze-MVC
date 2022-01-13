using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Neo4jClient;
using NapredneBP_Project.Models;
using Microsoft.AspNetCore.Http;

namespace NapredneBP_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IGraphClient _client;
        private readonly RedisService _redisService;

        public UserController(IGraphClient client, RedisService redisService)
        {
            _client = client;
            _redisService = redisService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Create1()
        {
            return View("Create");
        }

        public async Task<IActionResult> Create(User user)
        {
            await _client.Cypher.Create("(m:User $user)")
                    .WithParam("user", user)
                    .ExecuteWithoutResultsAsync();

            return View("Index");
        }

        public IActionResult Login1()
        {
            return View("Login");
        }

        public async Task<IActionResult> Login(User user)
        {
            var res = await _client.Cypher.Match("(u:User)")
                                .Where((User u) => u.Username == user.Username && u.Password == user.Password)
                                .Return(u => u.As<User>() ).ResultsAsync;

            if (res.First() != null)
            {
                HttpContext.Session.SetString("activeUser", user.Username);
                await _redisService.AddUser(user.Username);
                return View("Index1");
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            var value = HttpContext.Session.GetString("activeUser");
            await _redisService.DeleteUser(value);
            return View("Index");
        }

    }
}
