using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGpt.Models.DataModels
{
    public class DietitianMeal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Dietitian ID is required")]
        public string? DietitianID { get; set; }

        [Required(ErrorMessage = "Meal ID is required")]
        public string? MealID { get; set; }
    }
}
