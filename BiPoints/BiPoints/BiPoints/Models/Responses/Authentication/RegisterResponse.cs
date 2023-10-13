using System;

namespace BiPoints.Models.Responses.Authentication
{
    class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Language { get; set; }
    }
}
