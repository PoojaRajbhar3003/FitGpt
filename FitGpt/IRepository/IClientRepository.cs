using FitGpt.Models.ResponseModels;

namespace FitGpt.IRepository
{
    public interface IClientRepository
    {
        Task<RepositoryResponseModel<List<ClientDetailsResult>>> GetClientAsync(string userID);
        Task<RepositoryResponseModel<string>> DeleteclientDetailsAsync(string userID);
    }
}
