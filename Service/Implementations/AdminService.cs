using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly NeksaraDbContext _context;

        public AdminService(NeksaraDbContext context)
        {
            _context = context;
        }

        public List<Admin> Get(string search, string role)
        {
            var query = _context.Admins
                .Where(a => !a.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => 
                    a.Name.Contains(search) ||
                    a.Email.Contains(search));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(a => a.Role == role);
            }

            return query.OrderBy(a => a.Name).ToList();
        }

        public Admin GetById(int id)
        {
            return _context.Admins.Find(id);
        }

        public void Create(Admin admin)
        {
            admin.CreatedAt = DateTime.UtcNow;
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void Update(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var admin = _context.Admins.Find(id);
            admin.IsDeleted = true;

            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "Admin",
                EntityId = id,
                DeletedAt = DateTime.UtcNow
            });
          
            _context.SaveChanges();
        }
    }
}