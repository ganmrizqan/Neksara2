using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public string CoverImage { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ISDeleted { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
