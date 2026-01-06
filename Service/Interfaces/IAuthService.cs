namespace NeksaraArief.Service.Interfaces
{
    public interface IAuthService
    {
        Admin Login(string email, string password);
        List<Admin> GetAdmins();
        Admin GetAdminById(int id);
        void CreateAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int id);
    }
}