using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGpt.Models.DataModels
{
    public class FoodItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Food ID is required")]
        public string? FoodId { get; set; }

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
