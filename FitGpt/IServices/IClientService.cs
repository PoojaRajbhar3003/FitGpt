using FitGpt.Models.DataModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IServices
{
    public interface IClientService
    {
        Task<ServiceResponse<object>> CreateclientDetailsAsync(ClientDetails user);
        Task<ServiceResponse<string>> DeleteclientDetailsAsync(string userID);
        Task<ServiceResponse<List<ClientDetailsResult>>> GetClientAsync(string userID);
    }
}
