using FitGpt.Filter;
using FitGpt.IServices;
using FitGpt.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FitGpt.Controllers
{
    
    [Route("api/UserService")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) { 
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync(UserRequestModel user)
        {
            try
            {
                var response = await _userService.CreateUserAsync(user);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("ClientLogin")]
        public async Task<IActionResult> ClientLogin(LoginRequestModel user)
        {
            try
            {
                var response = await _userService.ClientLoginAsync(user);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("DietitianLogin")]
        public async Task<IActionResult> DietitianLoginAsync(LoginRequestModel user)
        {
            try
            {
                var response = await _userService.ClientLoginAsync(user);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
