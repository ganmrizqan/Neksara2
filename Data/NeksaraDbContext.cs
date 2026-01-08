using Microsoft.EntityFrameworkCore;
using NeksaraArief.Models;

namespace NeksaraArief.Data
{
    public class NeksaraDbContext : DbContext
    {
        public NeksaraDbContext(DbContextOptions<NeksaraDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicFeedback> TopicFeedbacks { get; set; }
        public DbSet<Testimoni> Testimoni { get; set; }
        public DbSet<ArchiveItem> ArchiveItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Topics)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    Id = 1,
                    Name = "Super Admin",
                    Email = "superadmin@neksara.id",
                    Password = "admin123",
                    Role = "SuperAdmin",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new AdminUser
                {
                    Id = 2,
                    Name = "Admin",
                    Email = "admin@neksara.id",
                    Password = "admin123",
                    Role = "Admin",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
