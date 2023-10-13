using BiPoints.DAL.Entities;

namespace BiPoints.API.Models
{
    public class AuthenticateEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserEntity User { get; set; }
    }
}
