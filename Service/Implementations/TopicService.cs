using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly NeksaraDbContext _context;

        public TopicService(NeksaraDbContext context)
        {
            _context = context;
        }

        public List<Topic> GetAll(string search, string sort, int? rating)
        {
            var topic = _context.Topics
                .Include(t => t.Category)
                .Where(t => !t.ISDeleted && !t.Category.ISDeleted)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(search))
            {
                topic = topic.Where(t => t.TopicName.Contains(search) || t.Category.CategoryName.Contains(search));
            }

            if (rating.HasValue)
            {
                topic = topic.Where(t => t.GetAverageRating() >= rating.Value);
            }
            
            topic = sort switch
            {
                "name-asc" => topic.OrderBy(t => t.TopicName),
                "name-desc" => topic.OrderByDescending(t => t.TopicName),
                "view_desc" => topic.OrderByDescending(t => t.ViewCount),
                "view_asc" => topic.OrderBy(t => t.ViewCount),
                _ => topic.OrderByDescending(t => t.CreatedAt)
            };
            
            return topic.ToList();
        }

        public Topic GetById(int id)
        {
            return _context.Topics.FirstOrDefault(t => t.TopicId == id);
        }

        public Topic GetDetail(int id)
        {
            return _context.Topics
                .Include(t => t.Category)
                .Include(t => t.TopicFeedbacks.Where(f => f.IsApproved))
                .FirstOrDefault(t => t.TopicId == id && !t.ISDeleted);
        }

        public void Create(Topic topic)
        {
            topic.CreatedAt = DateTime.UtcNow;
            topic.ViewCount = 0;
            topic.Rating = 0;

            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        public void Update(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic == null)
            {
                return;
            }

            topic.ISDeleted = true;
            
            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "Topic",
                EntityId = id,
                DeletedAt = DateTime.UtcNow
            });
            
            _context.SaveChanges();
        }

        public double GetAverageRating(int topicId)
        {
            return _context.TopicFeedbacks
                .Where(f => f.TopicId == topicId && f.IsApproved && !f.ISDeleted)
                .Average(f => (double?)f.Rating) ?? 0;
        }

        public void IncrementView(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic == null)
            {
                return;
            }

            topic.ViewCount++;
            _context.SaveChanges();
        }
    }
}