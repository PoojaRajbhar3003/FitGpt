using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitGpt.Models.DataModels
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Email is requierd")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(2, ErrorMessage = "Password too short")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Invalid password choosen")]
        public string? Password { get; set; }
        
        
        [Required(ErrorMessage = "Role Type is required")]
        public string? RoleType { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public string? UserID { get; set; }
    }
}
