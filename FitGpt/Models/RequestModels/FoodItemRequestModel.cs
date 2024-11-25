using System.ComponentModel.DataAnnotations;

namespace FitGpt.Models.RequestModels
{
    public class FoodItemRequestModel
    {
        [Required(ErrorMessage = "User ID is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Qty is required")]
        public double Qty { get; set; }

        [Required(ErrorMessage = "Kcal is required")]
        public double Kcal { get; set; }

        [Required(ErrorMessage = "Protein is required")]
        public double Protein { get; set; }

        [Required(ErrorMessage = "Fats is required")]
        public double Fats { get; set; }
    }
}
