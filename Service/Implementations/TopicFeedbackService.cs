using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class TopicFeedbackService : ITopicFeedbackService
    {
        private readonly NeksaraDbContext _context;

        public TopicFeedbackService(NeksaraDbContext context)
        {
            _context = context;
        }

        public void SubmitFeedback(TopicFeedback topicFeedback)
        {
            topicFeedback.IsApproved = false;
            topicFeedback.IsDeleted = false;
            topicFeedback.CreatedAt = DateTime.UtcNow;

            _context.TopicFeedbacks.Add(topicFeedback);
            _context.SaveChanges();
        }

        public List<TopicFeedback> GetPending()
        {
            return _context.TopicFeedbacks
                .Include(f => f.Topic)
                .Where(f => !f.IsDeleted && !f.IsApproved)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public void Approve(int id)
        {
            var topicFeedback = _context.TopicFeedbacks.Find(id);
            topicFeedback.IsApproved = true;
            _context.SaveChanges();
        }

        public void Reject(int id)
        {
            var topicFeedback = _context.TopicFeedbacks.Find(id);
            topicFeedback.IsDeleted = true;

            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "TopicFeedback",
                EntityId = id,
                DeletedAt = DateTime.UtcNow
            });
            _context.SaveChanges();
        }

        public List<TopicFeedback> GetApprovedByTopic(int topicId)
        {
            return _context.TopicFeedbacks
                .Where(f => 
                    f.TopicId == topicId && 
                    f.IsApproved && 
                    !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }
    }
}