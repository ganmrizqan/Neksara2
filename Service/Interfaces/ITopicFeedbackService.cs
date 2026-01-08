using NeksaraArief.Models;

namespace NeksaraArief.Service.Interfaces
{
    public interface ITopicFeedbackService
    {
        void SubmitFeedback(TopicFeedback topicFeedback);
        List<TopicFeedback> GetPending();
        void Approve(int id);
        void Reject(int id);
        List<TopicFeedback> GetApprovedByTopic(int topicId);
    }
}