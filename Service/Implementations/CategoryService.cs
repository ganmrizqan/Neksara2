using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly NeksaraDbContext _context;

        public CategoryService(NeksaraDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll(string search, string sort)
        {
            var categories = _context.Categories
                .Include(c => c.Topics.Where(t => !t.ISDeleted))
                .Where(c => !c.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => 
                    c.CategoryName.Contains(search) || 
                    c.Description.Contains(search));
            }

            categories = sort switch
            {
                "name-asc" => categories.OrderBy(c => c.CategoryName),
                "name-desc" => categories.OrderByDescending(c => c.CategoryName),
                "topic-desc" => categories.OrderByDescending(c => c.Topics.Count()),
                "topic-asc" => categories.OrderBy(c => c.Topics.Count()),
                "view_desc" => categories.OrderByDescending(c => c.ViewCount),
                "view_asc" => categories.OrderBy(c => c.ViewCount),
                _ => categories.OrderByDescending(c => c.CreatedAt)
            };

            return categories.ToList();
        }

        public Category GetDetail(int id, string search, string sort, int? rating)
        {
            var category = _context.Categories
                .Include(c => c.Topics.Where(t => !t.ISDeleted))
                .FirstOrDefault(c => c.CategoryId == id && !c.ISDeleted);

            if (category == null)
            {
                return null;
            }

            var topics = category.Topics.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                topics = topics.Where(t => 
                    t.TopicName.Contains(search) || 
                    t.Description.Contains(search));
            }

            if (rating.HasValue)
            {
                topics = topics.Where(t => t.ViewCount == rating.Value);
            }

            topics = sort switch
            {
                "name-asc" => topics.OrderBy(t => t.TopicName),
                "name-desc" => topics.OrderByDescending(t => t.TopicName),
                "view_desc" => topics.OrderByDescending(t => t.ViewCount),
                "view_asc" => topics.OrderBy(t => t.ViewCount),
                "rating_desc" => topics.OrderByDescending(t => t.ViewCount),
                "rating_asc" => topics.OrderBy(t => t.ViewCount),
                _ => topics.OrderByDescending(t => t.CreatedAt)
            };

            category.Topics = topics.ToList();
            return category;
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void Create(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            category.ViewCount = 0;
            category.ISDeleted = false;

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var category = _context.Categories
                .Include(c => c.Topics)
                .FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return;
            }

            category.ISDeleted = true;

            foreach (var topic in category.Topics)
            {
                topic.ISDeleted = true;

                _context.ArchiveItems.Add(new ArchiveItem
                {
                    Entity = "Topic",
                    EntityId = topic.TopicId,
                    DeletedAt = DateTime.UtcNow
                });
            }

            _context.ArchiveItems.Add(new ArchiveItem
            {
                Entity = "Category",
                EntityId = category.CategoryId,
                DeletedAt = DateTime.UtcNow
            });
            
            _context.SaveChanges();
        }

        public void IncrementView(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return;
            }

            category.ViewCount++;
            _context.SaveChanges();
        }

        public List<Category> GetPublic()
        {
            return _context.Categories
                .Where(c => !c.ISDeleted)
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public Category GetDetailPublic(int id)
        {
            return _context.Categories
                .Include(c => c.Topics.Where(t => !t.ISDeleted))
                .FirstOrDefault(c => 
                    c.CategoryId == id && !c.ISDeleted);
        }
    }
}