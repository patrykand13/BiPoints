using BiPoints.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BiPoints.API.Models
{
    public class AuthenticateEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserEntity User { get; set; }
    }
}
