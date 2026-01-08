using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll(string search, string sort);
        Category GetById(int id);

        Category GetDetail(
            int id,
            string search,
            string sort,
            int? rating
        );

        void Create(Category category);
        void Update(Category category);
        void IncrementView(int id);
        void SoftDelete(int id);

        List<Category> GetPublic();
        Category GetDetailPublic(int id);
    }
}