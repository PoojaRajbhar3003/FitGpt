using FitGpt.Filter;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FitGpt.Controllers
{
    [Authorize(Roles = "Client,Dietitian")]
    [Route("api/FoodItemService")]
    [ServiceFilter(typeof(CustomAuthorizeFilter))]
    [ApiController]
    public class FoodItemController: ControllerBase
    {
        private readonly IFoodItemService _foodItemService;

        public FoodItemController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("CreateFoodItem")]
        public async Task<IActionResult> CreateFoodItem(FoodItemRequestModel fooditem)
        {
            try
            {
                var response = await _foodItemService.CreateFoodItemAsync(fooditem);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetFoodItem")]
        public async Task<IActionResult> GetFoodItem(string foodid)
        {
            try
            {
                var response = await _foodItemService.GetFoodItemAsync(foodid);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllFoodItem")]
        public async Task<IActionResult> GetAllFoodItem()
        {
            try
            {
                var response = await _foodItemService.GetAllFoodItemAsync();
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem(string foodId)
        {
            try
            {
                var response = await _foodItemService.DeleteFoodItemAsync(foodId);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

    }
}
