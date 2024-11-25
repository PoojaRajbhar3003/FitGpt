using FitGpt.Filter;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using FitGpt.Models.RequestModels;
using FitGpt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitGpt.Controllers
{
    [Authorize(Roles = "Dietitian")]
    [Route("api/DietitianService")]
    [ServiceFilter(typeof(CustomAuthorizeFilter))]
    [ApiController]
    public class DietitianController: ControllerBase
    {
        private readonly IDietitianService _dietitianService;

        public DietitianController(IDietitianService dietitianService)
        {
            _dietitianService = dietitianService;
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("CreateDietitianDetails")]
        public async Task<IActionResult> CreateDietitianDetails(DietitianDetails user)
        {
            try
            {
                var response = await _dietitianService.CreateDietitianDetailsAsync(user);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteDietitianDetails")]
        public async Task<IActionResult> DeleteDietitianDetails(string userID)
        {
            try
            {
                var response = await _dietitianService.DeleteDietitianDetailsAsync(userID);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetDietitianByDietitianId")]
        public async Task<IActionResult> GetDietitianByDietitianId(string userID)
        {
            try
            {
                var response = await _dietitianService.GetDietitianAsync(userID);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("CreateDietitianClients")]
        public async Task<IActionResult> CreateDietitianClients(DietitianClientsRequestModel newRequest)
        {
            try
            {
                var response = await _dietitianService.CreateDietitianClientsAsync(newRequest);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteDietitianClients")]
        public async Task<IActionResult> DeleteDietitianClients(DietitianClientsRequestModel newRequest)
        {
            try
            {
                var response = await _dietitianService.DeleteDietitianClientsAsync(newRequest);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetDietitianClients")]
        public async Task<IActionResult> GetDietitianClients(DietitianClientsRequestModel newRequest)
        {
            try
            {
                var response = await _dietitianService.GetDietitianClientsAsync(newRequest);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetAllDietitianClients")]
        public async Task<IActionResult> GetAllDietitianClients(string DietitianId)
        {
            try
            {
                var response = await _dietitianService.GetAllDietitianClientsAsync(DietitianId);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpPut("AcceptClientsRequest")]
        public async Task<IActionResult> AcceptClientsRequest(DietitianClients newRequest)
        {
            try
            {
                var response = await _dietitianService.AcceptClientsRequestAsync(newRequest);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpPut("RejectClientsRequest")]
        public async Task<IActionResult> RejectClientsRequest(DietitianClients newRequest)
        {
            try
            {
                var response = await _dietitianService.AcceptClientsRequestAsync(newRequest);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
