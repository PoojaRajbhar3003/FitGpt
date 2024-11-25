using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGpt.Models.DataModels
{
    public class DietitianClients
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Dietitian ID is required")]
        public string? DietitianID { get; set; }

        [Required(ErrorMessage = "Client ID is required")]
        public string? ClientID { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int Status { get; set; }
    }
}
