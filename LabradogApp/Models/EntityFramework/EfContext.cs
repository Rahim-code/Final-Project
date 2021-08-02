using LabradogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabraDog.Models.EntityFramework
{
    public class EfContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-72FTU3Q\SQLEXPRESS01;Database=LabradogApp;Trusted_Connection=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<DidYouNow> DidYouNows { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewBlog> ReviewBlogs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Fag> Fags { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}