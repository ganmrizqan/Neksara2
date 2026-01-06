using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models;
{
    public class TopicView
    {
        [Key]
        public int TopicViewId { get; set; }

        [ForeignKey("TopicId")]
        public int TopicId { get; set; }

        public int UserId { get; set; }

        public DateTime ViewedAt { get; set; } = DateTime.Now;

        public Topic Topic { get; set; }
    }
}