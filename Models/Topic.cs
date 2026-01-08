using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Topic name cannot exceed 200 characters.")]
        public string TopicName { get; set; }

        public string Description { get; set; }
        public string? PictTopic { get; set; }
        public string VideoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ViewCount { get; set; }
        public bool ISDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<TopicFeedback> TopicFeedbacks { get; set; }
    }
}