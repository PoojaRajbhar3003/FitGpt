namespace FitGpt.Models.ResponseModels
{
    public class ClientDetailsResult
    {
        public string UserId { get; set; } 
        public string Email { get; set; }  
        public string Name { get; set; }   
        public string RoleType { get; set; } 
        public string Gender { get; set; }  
        public int? Age { get; set; }       
        public string Allergy { get; set; } 
        public string FoodPreference { get; set; } 
        public string MedicalCondition { get; set; } 
        public double? Height { get; set; }  
        public double? Weight { get; set; }
    }
}
