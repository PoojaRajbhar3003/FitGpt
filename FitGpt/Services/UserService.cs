using FitGpt.IRepository;
using FitGpt.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitGpt.Models.DataModels;
using FitGpt.Models.ResponseModels;
using FitGpt.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using FitGpt.Data;
using System.Text.RegularExpressions;
using AutoMapper;
namespace FitGpt.Services
{ 
    public class UserService : IUserService
    {

        private readonly IConfiguration Configuration;
        private readonly IGenericRepository<Users> _userRepository;
        private readonly FitGptDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IGenericRepository<Users> userRepository, FitGptDbContext dbContext, IMapper mapper)
        {
            Configuration = configuration;
            _userRepository = userRepository;
            _dbContext = dbContext;
            _mapper = mapper;   
        }

        private string GenerateJwtToken(string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Role, role) 
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(72),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> CreateId(string Roletype)
        {
            try
            {
                var records = await _dbContext.Users.AnyAsync();
                if (records)
                {
                    var LastId = await _dbContext.Users.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    int Id = int.Parse(Regex.Match(LastId.UserID, @"\d+").Value);
                    Id = Id + 1;
                    if (Roletype == "Client")
                    {
                        if (Id >= 10)
                            return "CLFG" + Id;
                        else
                            return "CLFG0" + Id;
                    }
                    else
                    {
                        if (Id >= 10)
                            return "DNFG" + Id;
                        else
                            return "DNFG0" + Id;
                    }
                    
                }
                else
                {
                    if (Roletype == "Client")
                        return "CLFG01";
                    else
                        return "DNFG01";
                }
                    
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<object>> CreateUserAsync(UserRequestModel user)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var IsExist = await _userRepository.GetAnyAsync("Email", user.Email);
                serviceResponse.Success = true;
                if (IsExist.Success)
                    serviceResponse.Message = "Account already exist with this email";
                else
                {
                    var newUser = _mapper.Map<Users>(user);
                    newUser.UserID = await CreateId(user.RoleType);
                    await _userRepository.PostAsync(newUser);
                    if (user.RoleType == "Client")
                    {
                        serviceResponse.Data = new
                        {
                            name = newUser.Name,
                            email = newUser.Email,
                            userID = newUser.UserID,
                            token = GenerateJwtToken(user.Email, "Client")
                        };
                    }
                    else
                    {
                        serviceResponse.Data = new
                        {
                            name = newUser.Name,
                            email = newUser.Email,
                            userID = newUser.UserID,
                            token = GenerateJwtToken(user.Email, "Dietitian")
                        };
                    }
                    serviceResponse.Message = "Account created successfully";
                }
                return serviceResponse;
            }
            catch (Exception ex) {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<object>> ClientLoginAsync(LoginRequestModel user)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var IsExist = await _userRepository.GetWhereAsync("Email", user.Email);

                serviceResponse.Success = true;
                if (!IsExist.Success)
                    serviceResponse.Message = "Account does not exist with this email";
                else
                {
                    if(IsExist.Data.Password == user.Password) {
                        serviceResponse.Data = new
                        {
                            name = IsExist.Data.Name,
                            email = IsExist.Data.Email,
                            userID = IsExist.Data.UserID,
                            token = GenerateJwtToken(user.Email, "Client")
                        };
                        serviceResponse.Message = "Login Successfully";
                    }
                    else
                        serviceResponse.Message = "Incorrect Password";
                }     
                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<ServiceResponse<object>> DietitianLoginAsync(LoginRequestModel user)
        {
            ServiceResponse<object> serviceResponse = new();
            try
            {
                var IsExist = await _userRepository.GetWhereAsync("Email", user.Email);

                serviceResponse.Success = true;
                if (!IsExist.Success)
                    serviceResponse.Message = "Account does not exist with this email";
                else
                {
                    if (IsExist.Data.Password == user.Password)
                    {
                        serviceResponse.Data = new
                        {
                            name = IsExist.Data.Name,
                            email = IsExist.Data.Email,
                            userID = IsExist.Data.UserID,
                            token = GenerateJwtToken(user.Email, "Dietitian")
                        };
                        serviceResponse.Message = "Login Successfully";
                    }
                    else
                        serviceResponse.Message = "Incorrect Password";

                }

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

    }
}
