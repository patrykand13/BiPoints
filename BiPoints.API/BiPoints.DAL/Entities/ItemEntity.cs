using System.ComponentModel.DataAnnotations;

namespace BiPoints.DAL.Entities
{
    public class ItemEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CodeQr { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Points { get; set; }
    }
}
