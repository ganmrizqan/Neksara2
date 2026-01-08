using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface IArchiveService
    {
        List<Category> GetDeletedCategories(string search);
        List<Topic> GetDeletedTopics(string search);
        List<Testimoni> GetDeletedTestimoni(string search);
        List<TopicFeedback> GetDeletedTopicFeedback(string search);
        void Restore(string entity, int id);
        void DeletePermanent(string entity, int id);
    }
}