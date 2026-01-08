using System.ComponentModel.DataAnnotations;

namespace NeksaraArief.Models
{
    public class SearchLog
    {
        [Key]
        public int SearchLogId { get; set; }
        public string SearchQuery { get; set; }
        public int ResultCount { get; set; }
        public DateTime SearchedAt { get; set; } = DateTime.Now;
    }
}