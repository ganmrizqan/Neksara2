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

        // =========================
        // ADMIN
        // =========================
        public List<Topic> GetAll(string search, string sort, int? rating)
        {
            var query = _context.Topics
                .Include(t => t.Category)
                .Where(t => !t.ISDeleted && !t.Category.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t =>
                    t.TopicName.Contains(search) ||
                    t.Category.CategoryName.Contains(search));
            }

            query = sort switch
            {
                "name-asc" => query.OrderBy(t => t.TopicName),
                "name-desc" => query.OrderByDescending(t => t.TopicName),
                "view-asc" => query.OrderBy(t => t.ViewCount),
                "view-desc" => query.OrderByDescending(t => t.ViewCount),
                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            var topics = query.ToList();

            // ⚠️ FILTER RATING SETELAH KE MEMORY
            if (rating.HasValue)
            {
                topics = topics
                    .Where(t => GetAverageRating(t.TopicId) >= rating.Value)
                    .ToList();
            }

            return topics;
        }

        public Topic GetById(int id)
        {
            return _context.Topics
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TopicId == id);
        }

        public Topic GetDetail(int id)
        {
            return GetById(id);
        }

        public void Create(Topic topic)
        {
            topic.CreatedAt = DateTime.UtcNow;
            topic.ViewCount = 0;
            topic.ISDeleted = false;

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
            if (topic == null) return;

            topic.ISDeleted = true;
            _context.SaveChanges();
        }

        public void IncrementView(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic == null) return;

            topic.ViewCount++;
            _context.SaveChanges();
        }

        // =========================
        // RATING (SERVICE LOGIC)
        // =========================
        public double GetAverageRating(int topicId)
        {
            return _context.TopicFeedbacks
                .Where(f => f.TopicId == topicId && f.IsApproved)
                .Average(f => (double?)f.Rating) ?? 0;
        }

        // =========================
        // USER
        // =========================
        public List<Topic> GetPopular(int take = 6)
        {
            return _context.Topics
                .Include(t => t.Category)
                .Where(t => !t.ISDeleted && !t.Category.ISDeleted)
                .OrderByDescending(t => t.ViewCount)
                .Take(take)
                .ToList();
        }

        public List<Topic> GetPublicByCategory(int categoryId)
        {
            return _context.Topics
                .Include(t => t.Category)
                .Where(t => !t.ISDeleted && !t.Category.ISDeleted && t.CategoryId == categoryId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }
    }
}


// using Microsoft.EntityFrameworkCore;
// using NeksaraArief.Data;
// using NeksaraArief.Models;
// using NeksaraArief.Service.Interfaces;

// namespace NeksaraArief.Service.Implementations
// {
//     public class TopicService : ITopicService
//     {
//         private readonly NeksaraDbContext _context;

//         public TopicService(NeksaraDbContext context)
//         {
//             _context = context;
//         }

//         public double GetAverageRating(int topicId)
//         {
//             return _context.TopicFeedbacks
//                 .Where(f => f.TopicId == topicId && f.IsApproved)
//                 .Average(f => (double?)f.Rating) ?? 0;
//         }

//         public List<Topic> GetAll(string search, string sort, int? rating)
//         {
//             var query = _context.Topics
//                 .Include(t => t.Category)
//                 .Where(t => !t.ISDeleted && !t.Category.ISDeleted)
//                 .AsQueryable();
            
//             if (!string.IsNullOrEmpty(search))
//             {
//                 query = query.Where(t => 
//                     t.TopicName.Contains(search) || 
//                     t.Category.CategoryName.Contains(search));
//             }

//             query = sort switch
//             {
//                 "name-asc" => query.OrderBy(t => t.TopicName),
//                 "name-desc" => query.OrderByDescending(t => t.TopicName),
//                 "view_desc" => query.OrderByDescending(t => t.ViewCount),
//                 "view_asc" => query.OrderBy(t => t.ViewCount),
//                 _ => query.OrderByDescending(t => t.CreatedAt)
//             };

//             var topics = query.ToList();
            
//             if (rating.HasValue)
//             {
//                 topics = topics
//                     .OrderByDescending(t => t.GetAverageRating(t.TopicId))
//                     .ToList();
//             }
            
//             return topics;
//         }

//         public Topic GetById(int? id)
//         {
//             return _context.Topics.FirstOrDefault(t => t.TopicId == id);
//         }

//         public Topic GetDetail(int? id)
//         {
//             return _context.Topics
//                 .Include(t => t.Category)
//                 .Include(t => t.TopicFeedbacks.Where(f => f.IsApproved))
//                 .FirstOrDefault(t => t.TopicId == id && !t.ISDeleted);
//         }

//         public void Create(Topic topic)
//         {
//             topic.CreatedAt = DateTime.UtcNow;
//             topic.ViewCount = 0;

//             _context.Topics.Add(topic);
//             _context.SaveChanges();
//         }

//         public void Update(Topic topic)
//         {
//             _context.Topics.Update(topic);
//             _context.SaveChanges();
//         }

//         public void SoftDelete(int id)
//         {
//             var topic = _context.Topics.Find(id);
//             if (topic == null)
//             {
//                 return;
//             }

//             topic.ISDeleted = true;
            
//             _context.ArchiveItems.Add(new ArchiveItem
//             {
//                 Entity = "Topic",
//                 EntityId = id,
//                 DeletedAt = DateTime.UtcNow
//             });
            
//             _context.SaveChanges();
//         }

//         public void IncrementView(int id)
//         {
//             var topic = _context.Topics.Find(id);
//             if (topic == null)
//             {
//                 return;
//             }

//             topic.ViewCount++;
//             _context.SaveChanges();
//         }

//         public List<Topic> GetPublicByCategory(int categoryId)
//         {
//             return _context.Topics
//                 .Where(t => 
//                     t.CategoryId == categoryId && 
//                     !t.ISDeleted)
//                 .OrderByDescending(t => t.CreatedAt)
//                 .ToList();
//         }

//         public List<Topic> GetPopular(int take = 6)
//         {
//             return _context.Topics
//                 .Include(t => t.Category)
//                 .Where(t => 
//                     !t.ISDeleted && 
//                     !t.Category.ISDeleted)
//                 .OrderByDescending(t => t.ViewCount)
//                 .Take(take)
//                 .ToList();
//         }
//     }
// }