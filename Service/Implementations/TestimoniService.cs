using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class TestimoniService : ITestimoniService
    {
        private readonly NeksaraDbContext _context;

        public TestimoniService(NeksaraDbContext context)
        {
            _context = context;
        }

        public void Submit(Testimoni testimoni)
        {
            testimoni.IsApproved = false;
            testimoni.ISDeleted = false;
            testimoni.CreatedAt = DateTime.UtcNow;

            _context.Testimoni.Add(testimoni);
            _context.SaveChanges();
        }

        public List<Testimoni> GetApproved()
        {
            return _context.Testimoni
                .Where(t => !t.IsApproved && !t.ISDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public List<Testimoni> GetPending(string search, string sort, int? rating)
        {
            var query = _context.Testimoni
                .Where(t => !t.IsApproved && !t.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => 
                    t.UserName.Contains(search) ||
                    t.Deskripsi.Contains(search));
            }

            if (rating.HasValue)
            {
                query = query.Where(t => t.Rating == rating);
            }

            query = sort switch
            {
                "name-asc" => query.OrderBy(t => t.UserName),
                "name-desc" => query.OrderByDescending(t => t.UserName),
                "view_desc" => query.OrderByDescending(t => t.Rating),
                "view_asc" => query.OrderBy(t => t.Rating),
                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            return query.ToList();
        }

        public void Approve(int id)
        {
            var testimoni = _context.Testimoni.Find(id);
            testimoni.IsApproved = true;
            _context.SaveChanges();
        }

        public void Reject(int id)
        {
            var testimoni = _context.Testimoni.Find(id);
            testimoni.ISDeleted = true;

            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "Testimoni",
                EntityId = id,
                DeletedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
        }
    }
}