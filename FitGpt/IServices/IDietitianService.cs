using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IServices
{
    public interface IDietitianService
    {
        Task<ServiceResponse<object>> CreateDietitianDetailsAsync(DietitianDetails user);
        Task<ServiceResponse<string>> DeleteDietitianDetailsAsync(string userID);
        Task<ServiceResponse<List<DietitianDetailsResult>>> GetDietitianAsync(string userID);
        Task<ServiceResponse<string>> CreateDietitianClientsAsync(DietitianClientsRequestModel newRequest);
        Task<ServiceResponse<string>> DeleteDietitianClientsAsync(DietitianClientsRequestModel newRequest);
        Task<ServiceResponse<DietitianClients>> GetDietitianClientsAsync(DietitianClientsRequestModel newRequest);
        Task<ServiceResponse<List<DietitianClients>>> GetAllDietitianClientsAsync(string DietitianId);
        Task<ServiceResponse<string>> AcceptClientsRequestAsync(DietitianClients newRequest);
        Task<ServiceResponse<string>> RejectClientsRequestAsync(DietitianClients newRequest);
    }
}
