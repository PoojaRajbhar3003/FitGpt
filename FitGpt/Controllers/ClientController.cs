using FitGpt.Filter;
using FitGpt.IServices;
using FitGpt.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FitGpt.Controllers
{
    [Authorize(Roles = "Client")]
    [Route("api/ClientService")]
    [ServiceFilter(typeof(CustomAuthorizeFilter))]
    [ApiController]
    public class ClientController: ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("CreateclientDetails")]
        public async Task<IActionResult> CreateclientDetails(ClientDetails user)
        {
            try
            {
                var response = _clientService.CreateclientDetailsAsync(user);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteclientDetails")]
        public async Task<IActionResult> CreateclientDetails(string userID)
        {
            try
            {
                var response = _clientService.DeleteclientDetailsAsync(userID);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("GetClientByClientId")]
        public async Task<IActionResult> GetClientByClientId(string userID)
        {
            try
            {
                var response = _clientService.GetClientAsync(userID);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
