namespace NeksaraArief.Service.Interfaces
{
    public interface ITopicService
    {
        List<Topic> GetAll(string search, string sort, int? rating);
        Topic GetById(int id);
        Topic GetDetail(int id);
        void Create(Topic topic);
        void Update(Topic topic);
        void SoftDelete(int id);
        double GetAverageRating(int topicId);
        void IncrementView(int id);
    }
}