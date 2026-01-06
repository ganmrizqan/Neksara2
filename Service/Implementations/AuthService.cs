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

        public Admin Login(string email, string password)
        {
            return _context.Admins.FirstOrDefault(a => 
                a.Email == email &&
                a.Password == password &&
                !a.IsDeleted);
        }

        public List<Admin> GetAdmins()
        {
            return _context.Admins
                .Where(a => !a.IsDeleted)
                .ToList();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Find(id);
        }

        public void CreateAdmin(Admin admin)
        {
            admin.CreatedAt = DateTime.UtcNow;
            admin.IsDeleted = false;

            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
        }

        public void DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null)
            {
                return;
            }

            admin.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
