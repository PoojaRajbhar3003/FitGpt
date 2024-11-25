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
    public class MealService: IMealService
    {
        private readonly IGenericRepository<Meal> _mealRepository;
        private readonly IGenericRepository<DietitianMeal> _dietitianMealRepository;
        private readonly IGenericRepository<ClientMealAssignByDietitian> _clientMealAssignByDietitian;
        private readonly FitGptDbContext _dbContext;
        private readonly IMapper _mapper;

        public MealService(IGenericRepository<Meal> MealRepository, FitGptDbContext dbContext, IMapper mapper,
            IGenericRepository<DietitianMeal> dietitianMealRepository,
            IGenericRepository<ClientMealAssignByDietitian> clientMealAssignByDietitian
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mealRepository = MealRepository;
            _dietitianMealRepository = dietitianMealRepository;
            _clientMealAssignByDietitian = clientMealAssignByDietitian;
        }

        private async Task<string> CreateId()
        {
            try
            {
                var records = await _dbContext.Meal.AnyAsync();
                if (records)
                {
                    var LastId = await _dbContext.Meal.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    int Id = int.Parse(Regex.Match(LastId.MealID, @"\d+").Value);
                    Id = Id + 1;
                    return "MFG" + Id;
                }
                else
                    return "MFG01";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }

        }

        public async Task<ServiceResponse<object>> CreateMealAsync(MealRequestModel meal)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var newId = await CreateId();
                var newMeal = _mapper.Map<Meal>(meal);
                newMeal.MealID = newId;
                var newDietitianMeal = _mapper.Map<DietitianMeal>(newMeal);
                await _mealRepository.PostAsync(newMeal);
                await _dietitianMealRepository.PostAsync(newDietitianMeal);

                serviceResponse.Message = "Meal Added Successfully";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<Meal>> GetMealAsync(string mealId)
        {
            ServiceResponse<Meal> serviceResponse = new();
            try
            {
                var newItem = await _mealRepository.GetWhereAsync("MealID", mealId);
                serviceResponse.Message = "Meal found";
                serviceResponse.Data = newItem.Data;
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<Meal>>> GetAllMealAsync()
        {
            ServiceResponse<List<Meal>> serviceResponse = new();
            try
            {
                var newItem = await _mealRepository.GetAll();
                serviceResponse.Message = "Meal found";
                serviceResponse.Data = newItem.Data;
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteMealAsync(string mealId)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var newItem = await _mealRepository.DeleteWhereAsync("MealID", mealId);
                serviceResponse.Message = "Food deleted successfully";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<Meal>>> GetAllDietitianMealAsync(string Id)
        {
            ServiceResponse<List<Meal>> serviceResponse = new();
            try
            {
                var newItem = await _dietitianMealRepository.GetAllWhereAsync("DietitianID",Id);
                List<Meal> newList = new();

                foreach (var item in newItem.Data)
                {
                    var newitem = await _mealRepository.GetWhereAsync("MealID", item.MealID);
                    if (newitem != null)
                    {
                        newList.Add(newitem.Data);
                    }
                }

                serviceResponse.Message = "Meal found";
                serviceResponse.Data = newList;
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<Meal>> GetDietitianMealByMealIdAsync(string mealId,string Id)
        {
            ServiceResponse<Meal> serviceResponse = new();
            try
            {
                var newItem = await _dietitianMealRepository.GetAllWhereAsync("DietitianID", Id);

                foreach (var item in newItem.Data)
                {
                    var newitem = await _mealRepository.GetWhereAsync("MealID", item.MealID);
                    if (newitem.Data.MealID == mealId)
                    {
                        serviceResponse.Data = newitem.Data;
                    }
                }

                serviceResponse.Message = "Meal found";
                
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteDietitianMealByMealIdAsync(string mealId, string Id)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var newItem = await _dietitianMealRepository.GetAllWhereAsync("DietitianID", Id);

                foreach (var item in newItem.Data)
                {
                    var newitem = await _mealRepository.GetWhereAsync("MealID", item.MealID);
                    if (newitem.Data.MealID == mealId)
                    {
                        await _dietitianMealRepository.DeleteWhereAsync("MealID", item.MealID);
                    }
                }

                serviceResponse.Message = "Dietitian Meal deleted";

                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> AssignMealToClient(ClientMealAssignByDietitian record)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                await _clientMealAssignByDietitian.PostAsync(record);
                serviceResponse.Success = true;
                serviceResponse.Message = "Meal Assign Successfully";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<string>> DeleteAssignMealToClient(ClientMealAssignByDietitian record)
        {
            ServiceResponse<string> serviceResponse = new();
            try
            {
                var itemList = await GetAllDietitianMealAssignToClient(record.DietitianID);
                foreach (var item in itemList.Data) { 
                    if(item.ClientID == record.ClientID)
                    {
                        await _clientMealAssignByDietitian.DeleteAsync(record);
                        serviceResponse.Success = true;
                        serviceResponse.Message = "Record Remove Successfully";
                    }
                }
                
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<List<ClientMealAssignByDietitian>>> GetAllClientMeal(string Id)
        {
            ServiceResponse<List<ClientMealAssignByDietitian>> serviceResponse = new();
            try
            {
                var items = await _clientMealAssignByDietitian.GetAllWhereAsync("ClientID",Id);
                serviceResponse.Data = items.Data;
                serviceResponse.Success = true;
                serviceResponse.Message = "Meal List found";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
        public async Task<ServiceResponse<List<ClientMealAssignByDietitian>>> GetAllDietitianMealAssignToClient(string Id)
        {
            ServiceResponse<List<ClientMealAssignByDietitian>> serviceResponse = new();
            try
            {
                var items = await _clientMealAssignByDietitian.GetAllWhereAsync("DietitianID", Id);
                serviceResponse.Data = items.Data;
                serviceResponse.Success = true;
                serviceResponse.Message = "Meal List found";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
