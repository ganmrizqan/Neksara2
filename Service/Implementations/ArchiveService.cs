using Microsoft.EntityFrameworkCore;
using NeksaraArief.Data;
using NeksaraArief.Models;
using NeksaraArief.Service.Interfaces;

namespace NeksaraArief.Service.Implementations
{
    public class ArchiveService : IArchiveService
    {
        private readonly NeksaraDbContext _context;

        public ArchiveService(NeksaraDbContext context)
        {
            _context = context;
        }

        public List<Category> GetDeletedCategories(string search)
        {
            var query = _context.Categories
                .Where(x => x.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CategoryName.Contains(search));
            }

            return query.ToList();
        }

        public List<Topic> GetDeletedTopics(string search)
        {
            var query = _context.Topics
            .Include(x => x.Category)
                .Where(x => x.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.TopicName.Contains(search));
            }

            return query.ToList();
        }

        public List<Testimoni> GetDeletedTestimoni(string search)
        {
            var query = _context.Testimoni
                .Where(x => x.ISDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => 
                    x.UserName.Contains(search) ||
                    x.Deskripsi.Contains(search)
                    );
            }

            return query.ToList();
        }

        public List<TopicFeedback> GetDeletedTopicFeedback(string search)
        {
            var query = _context.TopicFeedbacks
                .Include(x => x.Topic)
                .Where(x => x.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => 
                    x.UserName.Contains(search) ||
                    x.Comment.Contains(search)
                    );
            }

            return query.ToList();
        }

        public void Restore(string entity, int id)
        {
            switch (entity)
            {
                case "Category":
                    _context.Categories.Find(id).ISDeleted = false;
                    break;

                case "Topic":
                    _context.Topics.Find(id).ISDeleted = false;
                    break;

                case "TopicFeedback":
                    _context.TopicFeedbacks.Find(id).IsDeleted = false;
                    break;

                case "Testimoni":
                    _context.Testimoni.Find(id).ISDeleted = false;
                    break;
            }

            _context.SaveChanges();
        }

        public void DeletePermanent(string entity, int id)
        {
            switch (entity)
            {
                case "Category":
                    _context.Categories.Remove(_context.Categories.Find(id));
                    break;

                case "Topic":
                    _context.Topics.Remove(_context.Topics.Find(id));
                    break;

                case "TopicFeedback":
                    _context.TopicFeedbacks.Remove(_context.TopicFeedbacks.Find(id));
                    break;

                case "Testimoni":
                    _context.Testimoni.Remove(_context.Testimoni.Find(id));
                    break;
            }

            _context.ArchiveItems.Remove(_context.ArchiveItems.First(x => x.Entity == entity && x.EntityId == id));
            _context.SaveChanges();
        }
    }
}
