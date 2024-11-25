using FitGpt.Data;
using FitGpt.IRepository;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FitGpt.Services
{
    public class ClientService: IClientService
    {
        private readonly IGenericRepository<ClientDetails> _clientDetailsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly FitGptDbContext _dbContext;

        public ClientService( IGenericRepository<ClientDetails> clientDetailsRepository, FitGptDbContext dbContext,
            IClientRepository clientRepository)
        {
            _clientDetailsRepository = clientDetailsRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<object>> CreateclientDetailsAsync(ClientDetails user)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var IsExist = await _clientDetailsRepository.GetAnyAsync("UserID", user.UserID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                    serviceResponse.Message = "User details already exist";
                else
                {
                    await _clientDetailsRepository.PostAsync(user);
                    serviceResponse.Message = "User details added successfully";
                }
                   
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteclientDetailsAsync(string userID)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var IsExist = await _clientDetailsRepository.GetAnyAsync("UserID", userID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                {
                    var result = await _clientRepository.DeleteclientDetailsAsync(userID);
                    serviceResponse.Message = result.Data;
                }
                else
                    serviceResponse.Message = "User details does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<ClientDetailsResult>>> GetClientAsync(string userID)
        {
            ServiceResponse<List<ClientDetailsResult>> serviceResponse = new();
            try
            {
                var IsExist = await _clientDetailsRepository.GetAnyAsync("UserID", userID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                {
                    var data = await _clientRepository.GetClientAsync(userID);
                    serviceResponse.Data = data.Data;
                    serviceResponse.Message = "Deltails found";
                }
                else
                    serviceResponse.Message = "User does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
