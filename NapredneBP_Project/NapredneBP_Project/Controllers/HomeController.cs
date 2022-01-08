using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NapredneBP_Project.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGraphClient _client;

        public HomeController(IGraphClient client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
