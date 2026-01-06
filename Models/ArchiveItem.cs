using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models
{
    public class ArchiveItem
    {
        [Key]
        public int ArchiveItemId { get; set; }

        public string Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}