using NeksaraArief.Models;
using Microsoft.AspNetCore.Http;

namespace NeksaraArief.Service.Interfaces
{
    public interface ITopicService
    {
        List<Topic> GetAll(string? search, string? sort);
        Topic GetById(int id);
        // Topic GetDetail(int id);
        void Create(Topic topic, IFormFile pictTopic);
        void Update(Topic topic, IFormFile pictTopic);
        void SoftDelete(int id);
        // double GetAverageRating(int topicId);
        // void IncrementView(int id);

        // List<Topic> GetPublicByCategory(int categoryId);
        // List<Topic> GetPopular(int take = 6);
    }
}