using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface IAdminService
    {
        List<AdminUser> Get(string search, string role);
        AdminUser GetById(int id);
        void Create(AdminUser admin);
        void Update(AdminUser admin);
        void SoftDelete(int id);
    }
}