﻿using System.ComponentModel.DataAnnotations;

namespace moment2_mvc.Models
{
    public class RecipeModel
    {
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Titel är obligatoriskt")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Du måste lägga till ingredienser!")]
        public string? Ingredients { get; set; }

        [Required(ErrorMessage = "Instruktioner måste finnas!")]
        public string? Instructions { get; set; }
    }
}
