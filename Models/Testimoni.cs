namespace NeksaraArief.Models;
{
    public class Testimoni
    {
        [Key]
        public int TestimoniId { get; set; }

        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Deskripsi { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; }
        public bool ISDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}