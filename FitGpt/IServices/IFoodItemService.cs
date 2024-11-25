using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IServices
{
    public interface IFoodItemService
    {
        Task<ServiceResponse<string>> DeleteFoodItemAsync(string foodId);
        Task<ServiceResponse<FoodItem>> GetFoodItemAsync(string foodid);
        Task<ServiceResponse<List<FoodItem>>> GetAllFoodItemAsync();
        Task<ServiceResponse<object>> CreateFoodItemAsync(FoodItemRequestModel fooditem);
    }
}

