using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IServices
{
    public interface IUserService
    {
        Task<ServiceResponse<object>> CreateUserAsync(UserRequestModel user);
        Task<ServiceResponse<object>> DietitianLoginAsync(LoginRequestModel user);
        Task<ServiceResponse<object>> ClientLoginAsync(LoginRequestModel user);
    }
}
