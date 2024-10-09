using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiPoints.DAL.Entities
{
    public class PointEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public int BiPoints { get; set; }
        public int BiPointsForUse { get; set; }
        public int BiPointsUsed { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
