namespace NeksaraArief.Service.Interfaces
{
    public interface IAdminService
    {
        List<Admin> Get(string search, string role);
        Admin GetById(int id);
        void Create(Admin admin);
        void Update(Admin admin);
        void SoftDelete(int id);
    }
}