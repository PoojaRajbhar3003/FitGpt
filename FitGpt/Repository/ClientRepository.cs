using FitGpt.Data;
using FitGpt.IRepository;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FitGpt.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly FitGptDbContext _dbContext;
        

        public ClientRepository(FitGptDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepositoryResponseModel<List<ClientDetailsResult>>> GetClientAsync(string userID)
        {
            RepositoryResponseModel<List<ClientDetailsResult>> response = new();
            try
            {
                response.Data = await _dbContext.ClientDetailsResult
                                        .FromSqlRaw("SELECT * FROM \"getclient\"({0})", userID)
                                        .ToListAsync();
                response.Success = true;

                return response;
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }

        }

        public async Task<RepositoryResponseModel<string>> DeleteclientDetailsAsync(string userID)
        {
            RepositoryResponseModel<string> Response = new();
            try
            {
                var result = await _dbContext.FunctionResult
                                        .FromSqlRaw("SELECT * FROM \"deleteclientrecords\"({0})", userID)
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
    }
}
