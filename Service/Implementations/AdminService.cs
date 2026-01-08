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

        public List<AdminUser> Get(string search, string role)
        {
            var query = _context.AdminUsers
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

        public AdminUser GetById(int id)
        {
            return _context.AdminUsers.Find(id);
        }

        public void Create(AdminUser admin)
        {
            admin.CreatedAt = DateTime.UtcNow;
            _context.AdminUsers.Add(admin);
            _context.SaveChanges();
        }

        public void Update(AdminUser admin)
        {
            _context.AdminUsers.Update(admin);
            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var admin = _context.AdminUsers.Find(id);
            admin.IsDeleted = true;

            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "AdminUser",
                EntityId = id,
                DeletedAt = DateTime.UtcNow
            });
          
            _context.SaveChanges();
        }
    }
}