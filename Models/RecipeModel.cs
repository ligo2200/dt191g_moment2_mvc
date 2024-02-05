using System.ComponentModel.DataAnnotations;

namespace moment2_mvc.Models
{
    public class RecipeModel
    {
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Titel är obligatoriskt")]
        [Display(Name = "Titel:")]
        public string? Title { get; set; }

        [Display(Name = "Kategori:")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Du måste ange svårighetsgrad")]
        [Display(Name = "Svårighetsgrad:")]
        public string? Difficulty { get; set; }

        [Display(Name = "Vegetarisk:")]
        public string? IsVegetarian { get; set; }

        [Required(ErrorMessage = "Du måste lägga till ingredienser!")]
        [Display(Name = "Lägg till ingredienser:")]
        public string? Ingredients { get; set; }

        [Required(ErrorMessage = "Instruktioner måste finnas!")]
        [Display(Name = "Instruktioner:")]
        public string? Instructions { get; set; }
    }
}
