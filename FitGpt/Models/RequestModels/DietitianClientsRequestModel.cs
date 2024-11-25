using System.ComponentModel.DataAnnotations;

namespace FitGpt.Models.RequestModels
{
    public class DietitianClientsRequestModel
    {
        [Required(ErrorMessage = "Dietitian ID is required")]
        public string? DietitianID { get; set; }

        [Required(ErrorMessage = "Meal ID is required")]
        public string? ClientID { get; set; }
    }
}
