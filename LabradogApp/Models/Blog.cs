using System;
using System.Collections.Generic;

namespace LabradogApp.Models
{
    public class Blog : BaseEntity
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public List<BlogTag> BlogTags { get; set; }

        public List<ReviewBlog> ReviewBlogs { get; set; }

    }
}