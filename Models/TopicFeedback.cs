using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models
{
    public class TopicFeedback
    {
        [Key]
        public int TopicFeedbackId { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public string UserName { get; set; }
        public string UserRole { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}