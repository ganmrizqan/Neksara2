using NeksaraArief.Models;
using Microsoft.AspNetCore.Http;

namespace NeksaraArief.Service.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll(string? search, string? sort);
        Category GetById(int id);

        // Category GetDetail(
        //     int id,
        //     string search,
        //     string sort,
        //     int? rating
        // );

        void Create(Category category, IFormFile coverImage);
        void Update(Category category, IFormFile coverImage);
        // void IncrementView(int id);
        void SoftDelete(int id);

        // List<Category> GetPublic();
        // Category GetDetailPublic(int id);

        Category GetAdminDetail(int id);
        // List<Topic> GetAdminTopics(
        //     int id,
        //     string search,
        //     string sort
        // );
    }
}