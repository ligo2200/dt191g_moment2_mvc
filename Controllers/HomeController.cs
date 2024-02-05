using Microsoft.AspNetCore.Mvc;
using moment2_mvc.Models;
using System.Diagnostics;
// library for json
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace moment2_mvc.Controllers
{
    public class HomeController : Controller
    {
        // Path to json
        private readonly string _jsonFilePath = Path.Combine("wwwroot", "Data", "recipes.json");

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            string jsonStr = System.IO.File.ReadAllText(_jsonFilePath);

            var recipes = JsonSerializer.Deserialize<List<RecipeModel>>(jsonStr).OrderByDescending(r => r.RecipeId).Take(3).ToList();

            return View(recipes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
