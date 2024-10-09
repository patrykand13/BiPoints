using BiPoints.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiPoints.DAL.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Authenticate")]
        public Guid AuthenticateId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public int PhoneNumber { get; set; }
        public AuthenticateEntity Authenticate { get; set; }
        public List<ScanHistoryEntity> ScanHistories { get; set; }
        public PointEntity PointEntity { get; set; }
    }
}
