﻿using System.ComponentModel.DataAnnotations;

namespace FitGpt.Models.RequestModels
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is requierd")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(2, ErrorMessage = "Password too short")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Invalid password choosen")]
        public string? Password { get; set; }
    }
}