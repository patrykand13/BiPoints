using BiPoints.BLL.DTO.Response;
using BiPoints.BLL.DTO.Response.Authenticate;
using BiPoints.BLL.DTO.Response.User;
using BiPoints.BLL.Interfaces.Authenticate;
using BiPoints.DAL.Interfaces;
using BiPoints.DAL.Interfaces.Authenticate;
using BiPoints.DAL.Interfaces.Point;
using BiPoints.DAL.Interfaces.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BiPoints.BLL.Services.Authenticate
{
    public class AuthenticateServices : IAuthenticateServices
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateRepositories _authenticateRepositories;
        private readonly ISaveRepositories _saveRepositories;
        private readonly IGetUserByAuthenticateIdRepositories _getUserRepositories;
        private readonly ICreatePointRepositories _createPointRepositories;
        public AuthenticateServices(IConfiguration configuration, IAuthenticateRepositories authenticateRepositories, ISaveRepositories saveRepositories,
            IGetUserByAuthenticateIdRepositories getUserRepositories, ICreatePointRepositories createPointRepositories)
        {
            _configuration = configuration;
            _authenticateRepositories = authenticateRepositories;
            _saveRepositories = saveRepositories;
            _getUserRepositories = getUserRepositories;
            _createPointRepositories = createPointRepositories;
        }

        public async Task<BaseResponse> Login(string username, string password)
        {
            try
            {
                var authenticateId = _authenticateRepositories.GetAuthenticateId(username, password);
                if (authenticateId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorWrongUsernameOrPassword"
                    };
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var tokenToWrite = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],

                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMonths(30),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenToWrite);
                var userModel = _getUserRepositories.GetUserDataByAuthenticateId(authenticateId);
                if (userModel == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheUserDoesntExist"
                    };
                }
                var userResponse = new UserResponse
                {
                    Id = userModel.Id,
                    Token = token,
                    Name = userModel.Name,
                    Lastname = userModel.Lastname,
                    City = userModel.City,
                    Address = userModel.Address,
                    PhoneNumber = userModel.PhoneNumber,
                    Language = userModel.Language
                };
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };

            }
            catch
            {
                return new BaseResponse
                {
                    Status = "Error",
                    Message = "ErrorGetDataFailed"
                };
            }
        }
        public async Task<BaseResponse> Register(string username, string password, string name, string lastname)
        {
            try
            {
                var userExist = _authenticateRepositories.CheckUserExists(username);
                if (userExist)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorThisUserAlreadyExist"
                    };
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var tokenToWrite = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],

                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMonths(30),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenToWrite);

                var authenticateId = await _authenticateRepositories.CreateAuthenticate(username, password);
                if (authenticateId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorWrongUsernameOrPassword"
                    };
                }

                var userId = await _authenticateRepositories.CreateUser(authenticateId, name, lastname);
                if (userId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorCreateUserFailed"
                    };
                }
                var isSuccess = await _createPointRepositories.CreatePointByUserId(userId);
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorCreateUserFailed"
                    };
                }

                await _saveRepositories.Save();
                var userResponse = new RegisterResponse
                {
                    UserId = userId,
                    Token = token,
                    Name = name,
                    Lastname = lastname,
                    Language = "StringEnglish"
                };
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };

            }
            catch
            {
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorRegistrationFailed"
                };
            }
        }
    }
}
