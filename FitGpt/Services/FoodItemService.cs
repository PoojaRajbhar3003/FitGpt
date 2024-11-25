using AutoMapper;
using FitGpt.Data;
using FitGpt.IRepository;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FitGpt.Services
{
    public class FoodItemService: IFoodItemService
    {
        private readonly IGenericRepository<FoodItem> _foodItemRepository;
        private readonly FitGptDbContext _dbContext;
        private readonly IMapper _mapper;

        public FoodItemService(IGenericRepository<FoodItem> foodItemRepository, FitGptDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _foodItemRepository = foodItemRepository;
        }

        private async Task<string> CreateId()
        {
            try
            {
                var records = await _dbContext.FoodItem.AnyAsync();
                if (records)
                {
                    var LastId = await _dbContext.FoodItem.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    int Id = int.Parse(Regex.Match(LastId.FoodId, @"\d+").Value);
                    Id = Id + 1;
                    return "FIFG" + Id;
                }
                else
                    return "FIFG01";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }

        }

        public async Task<ServiceResponse<object>> CreateFoodItemAsync(FoodItemRequestModel fooditem)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var newId = await CreateId();
                var newFoodItem = _mapper.Map<FoodItem>(fooditem);
                newFoodItem.FoodId = newId;
                await _foodItemRepository.PostAsync(newFoodItem);
                serviceResponse.Message = "Fooed Item Added Successfully";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<FoodItem>> GetFoodItemAsync(string foodid)
        {
            ServiceResponse<FoodItem> serviceResponse = new();
            try
            {
                var newItem = await _foodItemRepository.GetWhereAsync("FoodId", foodid);
                serviceResponse.Message = "Food Item found";
                serviceResponse.Data= newItem.Data;
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<FoodItem>>> GetAllFoodItemAsync()
        {
            ServiceResponse<List<FoodItem>> serviceResponse = new();
            try
            {
                var newItem = await _foodItemRepository.GetAll();
                serviceResponse.Message = "Food Item found";
                serviceResponse.Data = newItem.Data;
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteFoodItemAsync(string foodId)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var newItem = await _foodItemRepository.DeleteWhereAsync("FoodId", foodId);
                serviceResponse.Message = "Food deleted successfully";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

    }
}
