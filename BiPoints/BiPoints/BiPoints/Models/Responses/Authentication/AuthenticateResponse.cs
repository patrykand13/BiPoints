using System;

namespace BiPoints.Models.Responses.Authentication
{
    class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
