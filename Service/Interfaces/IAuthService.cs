using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface IAuthService
    {
        AdminUser Login(string email, string password);
        List<AdminUser> GetAdminUsers();
        AdminUser GetAdminUserById(int id);
        void CreateAdminUser(AdminUser adminUser);
        void UpdateAdminUser(AdminUser adminUser);
        void DeleteAdminUser(int id);
    }
}