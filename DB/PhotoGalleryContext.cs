using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class PhotoGalleryContext : DbContext
    {
        private PhotoGalleryContext dbContext;

        public PhotoGalleryContext()  
        {
        }

        public PhotoGalleryContext(DbContextOptions<PhotoGalleryContext> options) : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Image> Customers { get; set; }
        public DbSet<User> Users { get; set; } 
         
    }
}
