using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGpt.Models.DataModels
{
    public class ClientDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "User ID is required")]
        public string? UserID { get; set; }
        
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        public string? Allergy { get; set; }
        
        [Required(ErrorMessage = "Food Prefrence is required")]
        public string? FoodPrefrence { get; set; }
        public string? MedicalCondition { get; set; }
        
        [Required(ErrorMessage = "Height is required")]
        public double Height { get; set; }
        
        [Required(ErrorMessage = "Weight is required")]
        public double Weight { get; set; }
    }
}
