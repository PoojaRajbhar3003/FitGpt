using FitGpt.Data;
using FitGpt.IRepository;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FitGpt.Repository
{
    public class DietitianRepository: IDietitianRepository
    {
        private readonly FitGptDbContext _dbContext;

        public DietitianRepository(FitGptDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepositoryResponseModel<List<DietitianDetailsResult>>> GetDietitianAsync(string userID)
        {
            RepositoryResponseModel<List<DietitianDetailsResult>> response = new();
            response.Data = await _dbContext.DietitianDetailsResult
                                        .FromSqlRaw("SELECT * FROM \"getdietitian\"({0})", userID)
                                        .ToListAsync();
            response.Success = true;

            return response;
        }

        public async Task<RepositoryResponseModel<string>> DeleteDietitianAsync(string userID)
        {
            RepositoryResponseModel<string> Response = new();
            try
            {
                var result= await _dbContext.FunctionResult
                                        .FromSqlRaw("SELECT * FROM \"deletedietitianrecords\"({0})", userID)
                                        .ToListAsync();
                Response.Success = true;
                Response.Data = string.Join(", ", result);
                return Response;    
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<DietitianClients> GetDietitianClients(DietitianClientsRequestModel newRequest)
        {
            try
            {
                return await _dbContext.DietitianClients.FirstOrDefaultAsync(x => x.DietitianID == newRequest.DietitianID && x.ClientID == newRequest.ClientID);
            }
            catch (Exception ex) { throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<List<DietitianClients>> GetAllDietitianClients(string DietitianId)
        {
            try
            {
                return await _dbContext.DietitianClients.Where(x => x.DietitianID == DietitianId).ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<string> CreateDietitianClientsAsync(DietitianClients newClient)
        {
            try
            {
                await _dbContext.DietitianClients.AddAsync(newClient);
                await _dbContext.SaveChangesAsync();
                return "true";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }


        public async Task<string> UpdateClientsRequestAsync(DietitianClients newRequest)
        {
            try
            {
                _dbContext.DietitianClients.Update(newRequest);
                await _dbContext.SaveChangesAsync();
                return "true";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<string> DeleteClientsRequestAsync(DietitianClients newRequest)
        {
            try
            {
                _dbContext.DietitianClients.Remove(newRequest);
                await _dbContext.SaveChangesAsync();
                return "true";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
