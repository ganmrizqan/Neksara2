using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace NeksaraArief.Service.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly NeksaraDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TopicService(NeksaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<Topic> GetAll(string? search, string? sort)
        {
            var query = _context.Topics
                .Include(t => t.Category)
                .Where(t => !t.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t =>
                    t.TopicName.Contains(search));
            }

            query = sort switch
            {
                "name-asc" => query.OrderBy(t => t.TopicName),
                "name-desc" => query.OrderByDescending(t => t.TopicName),

                "view-asc" => query.OrderBy(t => t.ViewCount),
                "view-desc" => query.OrderByDescending(t => t.ViewCount),
                
                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            return query.ToList();
        }

        public Topic GetById(int id)
        {
            return _context.Topics
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TopicId == id);
        }

        public void Create(Topic topic, IFormFile pictTopic)
        {
            topic.CreatedAt = DateTime.UtcNow;
            topic.ViewCount = 0;
            topic.ISDeleted = false;

            if (pictTopic != null && pictTopic.Length > 0)
            {
                topic.PictTopic = SaveImage(pictTopic);
            }

            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        public void Update(Topic topic, IFormFile pictTopic)
        {
            var existing = _context.Topics.First(t => t.TopicId == topic.TopicId);

            existing.TopicName = topic.TopicName;
            existing.CategoryId = topic.CategoryId;
            existing.Description = topic.Description;
            existing.VideoUrl = topic.VideoUrl;
            existing.UpdatedAt = DateTime.UtcNow;

            if (pictTopic != null)
            {
                existing.PictTopic = SaveImage(pictTopic);
            }

            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var topic = _context.Topics.First(t => t.TopicId == id);

            topic.ISDeleted = true;
        
            _context.SaveChanges();
        }

        private string SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/topics");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return $"/uploads/topics/{fileName}";
        }
    }
}

    //     public void IncrementView(int id)
    //     {
    //         var topic = _context.Topics.Find(id);
    //         if (topic == null) return;

    //         topic.ViewCount++;
    //         _context.SaveChanges();
    //     }

    //     // =========================
    //     // RATING (SERVICE LOGIC)
    //     // =========================
    //     public double GetAverageRating(int topicId)
    //     {
    //         return _context.TopicFeedbacks
    //             .Where(f => f.TopicId == topicId && f.IsApproved)
    //             .Average(f => (double?)f.Rating) ?? 0;
    //     }

    //     // =========================
    //     // USER
    //     // =========================
    //     public List<Topic> GetPopular(int take = 6)
    //     {
    //         return _context.Topics
    //             .Include(t => t.Category)
    //             .Where(t => !t.ISDeleted && !t.Category.ISDeleted)
    //             .OrderByDescending(t => t.ViewCount)
    //             .Take(take)
    //             .ToList();
    //     }

    //     public List<Topic> GetPublicByCategory(int categoryId)
    //     {
    //         return _context.Topics
    //             .Include(t => t.Category)
    //             .Where(t => !t.ISDeleted && !t.Category.ISDeleted && t.CategoryId == categoryId)
    //             .OrderByDescending(t => t.CreatedAt)
    //             .ToList();
    //     }
    // }


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