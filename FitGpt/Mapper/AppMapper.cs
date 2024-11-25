using AutoMapper;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;

namespace FitGpt.Mapper
{
    public class AppMapper: Profile
    {
        public AppMapper()
        {
            CreateMap<UserRequestModel, Users>();
            CreateMap<DietitianClientsRequestModel, DietitianClients>();
            CreateMap<DietitianClients, DietitianClientsRequestModel>();
            CreateMap<FoodItemRequestModel, FoodItem>();
            CreateMap<MealRequestModel, Meal>();
            CreateMap<MealRequestModel, DietitianMeal>();

        }
    }
}
