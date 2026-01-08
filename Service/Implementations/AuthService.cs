using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly NeksaraDbContext _context;

        public AuthService(NeksaraDbContext context)
        {
            _context = context;
        }

        public AdminUser Login(string email, string password)
        {
            return _context.AdminUsers
                .FirstOrDefault(a => 
                    a.Email == email &&
                    a.Password == password &&
                    !a.IsDeleted);
        }

        public List<AdminUser> GetAdminUsers()
        {
            return _context.AdminUsers
                .Where(a => !a.IsDeleted)
                .ToList();
        }

        public AdminUser GetAdminUserById(int id)
        {
            return _context.AdminUsers.Find(id);
        }

        public void CreateAdminUser(AdminUser adminUser)
        {
            adminUser.CreatedAt = DateTime.UtcNow;
            adminUser.IsDeleted = false;

            _context.AdminUsers.Add(adminUser);
            _context.SaveChanges();
        }

        public void UpdateAdminUser(AdminUser adminUser)
        {
            _context.AdminUsers.Update(adminUser);
            _context.SaveChanges();
        }

        public void DeleteAdminUser(int id)
        {
            var adminUser = _context.AdminUsers.Find(id);
            if (adminUser == null)
            {
                return;
            }

            adminUser.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
