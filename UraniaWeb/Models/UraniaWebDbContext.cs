using Microsoft.EntityFrameworkCore;    

namespace UraniaWeb.Models
{
    public class UraniaWebDbContext : DbContext
    {
        public UraniaWebDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Admin>().ToTable("Admin");
            modelbuilder.Entity<Article>().ToTable("Article");
            modelbuilder.Entity<Slider>().ToTable("Slider");
        }
    }
}
