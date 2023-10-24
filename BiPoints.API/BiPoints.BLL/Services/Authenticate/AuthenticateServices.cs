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
                // Retrieve the authentication identifier based on the username and password.
                var authenticateId = _authenticateRepositories.GetAuthenticateId(username, password);

                // If the authentication identifier is empty, it indicates an authentication error.
                if (authenticateId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorWrongUsernameOrPassword"
                    };
                }

                // Create an access token upon successful authentication.
                var token = CreateToken();

                // Retrieve user data based on the authentication identifier.
                var userModel = _getUserRepositories.GetUserDataByAuthenticateId(authenticateId);

                // If user data is empty, it means the user does not exist.
                if (userModel == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheUserDoesntExist"
                    };
                }

                // Create a response containing user data and access token.
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

                // Return a  user data.
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };
            }
            catch
            {
                // If an error occurs during processing, return an error response.
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
                // Check if a user with the provided username already exists in the system.
                var userExist = _authenticateRepositories.CheckUserExists(username);

                // If a user with the given username already exists, return an error.
                if (userExist)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorThisUserAlreadyExist"
                    };
                }

                // Create an access token
                var token = CreateToken();

                // Create a user authentication identifier based on the username and password.
                var authenticateId = await _authenticateRepositories.CreateAuthenticate(username, password);

                // If the authentication identifier is empty, return an error.
                if (authenticateId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorWrongUsernameOrPassword"
                    };
                }

                // Create a user based on the authentication identifier and user data.
                var userId = await _authenticateRepositories.CreateUser(authenticateId, name, lastname);

                // If the user identifier is empty, return an error.
                if (userId == Guid.Empty)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorCreateUserFailed"
                    };
                }

                var isSuccess = await _createPointRepositories.CreatePointByUserId(userId);

                // If the create operation failed, return an error.
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorCreateUserFailed"
                    };
                }

                // Save the changes in the repository.
                await _saveRepositories.Save();

                // Create a response containing the user identifier, access token, and user data.
                var userResponse = new RegisterResponse
                {
                    UserId = userId,
                    Token = token,
                    Name = name,
                    Lastname = lastname,
                    Language = "StringEnglish"
                };

                // Return a success response with user data.
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = userResponse
                };
            }
            catch
            {
                // If an error occurred during processing, return an internal error response.
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorRegistrationFailed"
                };
            }
        }
        public string CreateToken()
        {
            // Create an authentication key based on the secret from configuration.
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // Create a JWT token, specifying the issuer, audience, expiration, and signing information.
            var tokenToWrite = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(30),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // Save the JWT token as a string and return it as the result.
            return new JwtSecurityTokenHandler().WriteToken(tokenToWrite);
        }

    }
}
