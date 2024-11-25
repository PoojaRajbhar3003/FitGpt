using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;

namespace FitGpt.IRepository
{
    public interface IDietitianRepository
    {
        Task<RepositoryResponseModel<List<DietitianDetailsResult>>> GetDietitianAsync(string userID);
        Task<List<DietitianClients>> GetAllDietitianClients(string DietitianId);
        Task<DietitianClients> GetDietitianClients(DietitianClientsRequestModel newRequest);
        Task<string> CreateDietitianClientsAsync(DietitianClients newClient);
        
        Task<string> UpdateClientsRequestAsync(DietitianClients newRequest);
        Task<RepositoryResponseModel<string>> DeleteDietitianAsync(string userID);
        Task<string> DeleteClientsRequestAsync(DietitianClients newRequest);
    }
}
