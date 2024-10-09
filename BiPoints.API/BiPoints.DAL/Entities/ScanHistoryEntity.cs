using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiPoints.DAL.Entities
{
    public class ScanHistoryEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        public string CodeQr { get; set; }
        public int Points { get; set; }
        public bool ScanSuccess { get; set; }
        public DateTime AddDate { get; set; }
        public UserEntity UserEntity { get; set; }
        [ForeignKey("CodeQr")]
        public ItemEntity Item { get; set; }
    }
}
