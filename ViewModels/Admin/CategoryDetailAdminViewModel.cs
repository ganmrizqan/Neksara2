using System.Collections.Generic;
using System.Linq;
using NeksaraArief.Models;

namespace NeksaraArief.ViewModels.Admin
{
    public class CategoryDetailAdminViewModel
    {
        public Category Category { get; set; }
        public List<Topic> Topics { get; set; }

        public int TotalTopic => Topics?.Count ?? 0;
        public int TotalViews => Topics?.Sum(x => x.ViewCount) ?? 0;
    }
}
