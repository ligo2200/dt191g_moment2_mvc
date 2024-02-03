using Microsoft.AspNetCore.Mvc;
using moment2_mvc.Models;
// library for json
using System.Text.Json;
// library for using lists
using System.Collections.Generic;
// library for reading and writing to file
using System.IO;
// library for lists (like sorting)
using System.Linq;
using System;

namespace moment2_mvc.Controllers
{
    public class RecipeController : Controller
    {
        private string _jsonFilePath;

        public RecipeController()
        {
            _jsonFilePath = Path.Combine("wwwroot", "Data", "recipes.json");
            // filepath for json-file
            //_jsonFilePath = @"recipes.json";
        }

        [Route("/skaparecept")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RecipeModel recipeModel)
        {
            Console.WriteLine(recipeModel);
            try
            {
                // if form is valid, add new recipe.
                if (ModelState.IsValid)
                {
                    var recipes = LoadRecipes();
                    recipeModel.RecipeId = recipes.Count + 1;
                    recipes.Add(recipeModel);
                    SaveRecipes(recipes);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If not valid, show form with errormessages.
                    return View(recipeModel);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Fel vid skapandet av recept: {ex.Message}", ex);

                return View("Error");
            }

            
        }

        [Route("/recept/{recipeId}")]
        public IActionResult Details(int recipeId)
        {
            var recipes = LoadRecipes();
            var recipe = recipes.FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                return View("RecipeNotFound");
            }

            return View(recipe);
        }

        [Route("/allarecept")]
        public IActionResult All()
        {
            // load recipes
            var recipes = LoadRecipes();

            // Sort after title
            var sortedRecipes = recipes.OrderBy(r => r.Title).ToList();

            return View(sortedRecipes);
        }

        private List<RecipeModel> LoadRecipes()
        {
            try
            {
                if (!System.IO.File.Exists(_jsonFilePath))
                {
                    // return empty list if file doesn't exist
                    return new List<RecipeModel>();
                }

                // read json file
                var json = System.IO.File.ReadAllText(_jsonFilePath);
                // return deserialized JSON recipelist 
                return JsonSerializer.Deserialize<List<RecipeModel>>(json);
            }
            catch (Exception ex)
            {
                // error message in console, returning empty list
                Console.WriteLine($"Fel vid inläsning av fil: {ex.Message}");
                return new List<RecipeModel>();
            }
        }
        // saving recipes to json
        private void SaveRecipes(List<RecipeModel> recipes)
        {
            // serialize list of recipes to JSON
            var json = JsonSerializer.Serialize(recipes, new JsonSerializerOptions { WriteIndented = true });
            // write JSON to file
            System.IO.File.WriteAllText(_jsonFilePath, json);
        }
    }
}
