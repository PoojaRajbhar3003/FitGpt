using FitGpt.Filter;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FitGpt.Controllers
{
    [Authorize(Roles = "Client,Dietitian")]
    [Route("api/MealService")]
    [ServiceFilter(typeof(CustomAuthorizeFilter))]
    [ApiController]
    public class MealController: ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("CreateMeal")]
        public async Task<IActionResult> CreateMealAsync(MealRequestModel meal)
        {
            try
            {
                var response = await _mealService.CreateMealAsync(meal);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetMeal")]
        public async Task<IActionResult> GetMealAsync(string mealId)
        {
            try
            {
                var response = await _mealService.GetMealAsync(mealId);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllMeal")]
        public async Task<IActionResult>  GetAllMeal()
        {
            try
            {
                var response = await _mealService.GetAllMealAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteMeal")]
        public async Task<IActionResult> DeleteMealAsync(string mealId)
        {
            try
            {
                var response = await _mealService.DeleteMealAsync(mealId);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllDietitianMeal")]
        public async Task<IActionResult> GetAllDietitianMealAsync(string Id)
        {
            try
            {
                var response = await _mealService.GetAllDietitianMealAsync(Id);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetDietitianMealByMealId")]
        public async Task<IActionResult> GetDietitianMealByMealIdAsync(string mealId, string Id)
        {
            try
            {
                var response = await _mealService.GetDietitianMealByMealIdAsync(mealId, Id);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteDietitianMealByMealId")]
        public async Task<IActionResult> DeleteDietitianMealByMealIdAsync(string mealId, string Id)
        {
            try
            {
                var response = await _mealService.DeleteDietitianMealByMealIdAsync(mealId, Id);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("AssignMealToClient")]
        public async Task<IActionResult> AssignMealToClient(ClientMealAssignByDietitian record)
        {
            try
            {
                var response = await _mealService.AssignMealToClient(record);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteAssignMealToClient")]
        public async Task<IActionResult> DeleteAssignMealToClient(ClientMealAssignByDietitian record)
        {
            try
            {
                var response = await _mealService.DeleteAssignMealToClient(record);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllClientMeal")]
        public async Task<IActionResult> GetAllClientMeal(string Id)
        {
            try
            {
                var response = await _mealService.GetAllClientMeal(Id);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllDietitianMealAssignToClient")]
        public async Task<IActionResult> GetAllDietitianMealAssignToClient(string Id)
        {
            try
            {
                var response = await _mealService.GetAllDietitianMealAssignToClient(Id);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
