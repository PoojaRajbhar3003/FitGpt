using System.ComponentModel.DataAnnotations;

namespace FitGpt.Models.RequestModels
{
    public class MealRequestModel
    {
        [Required(ErrorMessage = "Food ID is required")]
        public string? DietitianID { get; set; }

        [Required(ErrorMessage = "Food ID is required")]
        public string? FoodId { get; set; }

        [Required(ErrorMessage = "Qty is required")]
        public double Qty { get; set; }

        [Required(ErrorMessage = "Total Kcal is required")]
        public double TotalKcal { get; set; }

        [Required(ErrorMessage = "Total Protein is required")]
        public double TotalProtein { get; set; }

        [Required(ErrorMessage = "Total Fats is required")]
        public double TotalFats { get; set; }

        [Required(ErrorMessage = "Meal Name is required")]
        public string? MealName { get; set; }

        [Required(ErrorMessage = "Give time preference")]
        public bool IsBreakfast { get; set; }
        [Required(ErrorMessage = "Give time preference")]
        public bool IsLunch { get; set; }
        [Required(ErrorMessage = "Give time preference")]
        public bool IsEveningSnacks { get; set; }

        [Required(ErrorMessage = "Give time preference")]
        public bool IsDinner { get; set; }
    }
}
