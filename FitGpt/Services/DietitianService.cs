using FitGpt.Data;
using AutoMapper;
using FitGpt.IRepository;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FitGpt.Services
{
    public class DietitianService: IDietitianService
    {
        private readonly IGenericRepository<DietitianDetails> _dietitianDetailsRepository;
        private readonly IGenericRepository<DietitianClients> _DietitianClientsRepository;
        private readonly IDietitianRepository _dietitianRepository;
        private readonly FitGptDbContext _dbContext;
        private readonly IMapper _mapper;

        public DietitianService(IGenericRepository<DietitianDetails> dietitianDetailsRepository, 
            FitGptDbContext dbContext, IMapper mapper, IGenericRepository<DietitianClients> DietitianClientsRepository,
            IDietitianRepository dietitianRepository)
        {
            _dietitianDetailsRepository = dietitianDetailsRepository;
            _dbContext = dbContext;
            _mapper = mapper;
            _DietitianClientsRepository = DietitianClientsRepository;
            _dietitianRepository = dietitianRepository;
        }

        public async Task<ServiceResponse<object>> CreateDietitianDetailsAsync(DietitianDetails user)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianDetailsRepository.GetAnyAsync("UserID", user.UserID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                    serviceResponse.Message = "User details already exist";
                else
                {
                    await _dietitianDetailsRepository.PostAsync(user);
                    serviceResponse.Message = "User details added successfully";
                }

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteDietitianDetailsAsync(string userID)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianDetailsRepository.GetAnyAsync("UserID", userID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                {
                    var result = await _dietitianRepository.DeleteDietitianAsync(userID);
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

        public async Task<ServiceResponse<List<DietitianDetailsResult>>> GetDietitianAsync(string userID)
        {
            ServiceResponse<List<DietitianDetailsResult>> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianDetailsRepository.GetAnyAsync("UserID", userID);
                serviceResponse.Success = true;
                if (IsExist.Success)
                {
                    var data = await _dietitianRepository.GetDietitianAsync(userID);
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

        public async Task<ServiceResponse<string>> CreateDietitianClientsAsync(DietitianClientsRequestModel newRequest)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianRepository.GetDietitianClients(newRequest);
                serviceResponse.Success = true;
                if (IsExist == null)
                {
                    var request = _mapper.Map<DietitianClients>(newRequest);
                    request.Status = 0;
                    await _dietitianRepository.CreateDietitianClientsAsync(request);
                    serviceResponse.Message = "Client request added successfully";
                }
                else
                    serviceResponse.Message = "Client request already exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
        public async Task<ServiceResponse<string>> DeleteDietitianClientsAsync(DietitianClientsRequestModel newRequest)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianRepository.GetDietitianClients(newRequest);
                serviceResponse.Success = true;
                if (IsExist != null)
                {
                    await _dietitianRepository.DeleteClientsRequestAsync(IsExist);
                    serviceResponse.Message = "Client request remove successfully";
                }
                else
                    serviceResponse.Message = "Client request does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<DietitianClients>> GetDietitianClientsAsync(DietitianClientsRequestModel newRequest)
        {
            ServiceResponse<DietitianClients> serviceResponse = new();
            try
            {
                var IsExist = await _dietitianRepository.GetDietitianClients(newRequest);
                serviceResponse.Success = true;
                if (IsExist != null)
                {
                    serviceResponse.Data = IsExist;
                    serviceResponse.Message = "Client request found";
                }
                else
                    serviceResponse.Message = "Client request does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<DietitianClients>>> GetAllDietitianClientsAsync(string DietitianId)
        {
            ServiceResponse<List<DietitianClients>> serviceResponse = new();
            try
            {
                var ClientList = await _dietitianRepository.GetAllDietitianClients(DietitianId);
                serviceResponse.Success = true;
                if (ClientList != null)
                {
                    serviceResponse.Data = ClientList;
                    serviceResponse.Message = "Client requests found";
                }
                else
                    serviceResponse.Message = "Client requests does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> AcceptClientsRequestAsync(DietitianClients newRequest)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var request = _mapper.Map<DietitianClientsRequestModel>(newRequest);
                var IsExist = await _dietitianRepository.GetDietitianClients(request);
                serviceResponse.Success = true;
                if (IsExist != null)
                {
                    IsExist.Status = 1;
                    await _dietitianRepository.UpdateClientsRequestAsync(IsExist);
                    serviceResponse.Message = "Client request accepted found";
                }
                else
                    serviceResponse.Message = "Client request does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> RejectClientsRequestAsync(DietitianClients newRequest)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var request = _mapper.Map<DietitianClientsRequestModel>(newRequest);
                var IsExist = await _dietitianRepository.GetDietitianClients(request); 
                serviceResponse.Success = true;
                if (IsExist != null)
                {
                    IsExist.Status = 2;
                    _dbContext.DietitianClients.Update(IsExist);
                    await _dbContext.SaveChangesAsync();
                    serviceResponse.Message = "Client request accepted found";
                }
                else
                    serviceResponse.Message = "Client request does not exist";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        

    }
}
