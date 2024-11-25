using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IServices
{
    public interface IMealService
    {
        Task<ServiceResponse<object>> CreateMealAsync(MealRequestModel meal);
        Task<ServiceResponse<Meal>> GetMealAsync(string mealId);
        Task<ServiceResponse<List<Meal>>> GetAllMealAsync();
        Task<ServiceResponse<string>> DeleteMealAsync(string mealId);
        Task<ServiceResponse<List<Meal>>> GetAllDietitianMealAsync(string Id);
        Task<ServiceResponse<Meal>> GetDietitianMealByMealIdAsync(string mealId, string Id);
        Task<ServiceResponse<string>> DeleteDietitianMealByMealIdAsync(string mealId, string Id);
        Task<ServiceResponse<string>> AssignMealToClient(ClientMealAssignByDietitian record);
        Task<ServiceResponse<string>> DeleteAssignMealToClient(ClientMealAssignByDietitian record);
        Task<ServiceResponse<List<ClientMealAssignByDietitian>>> GetAllClientMeal(string Id);
        Task<ServiceResponse<List<ClientMealAssignByDietitian>>> GetAllDietitianMealAssignToClient(string Id);
    }
}
