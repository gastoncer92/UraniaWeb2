using Microsoft.EntityFrameworkCore;    

namespace UraniaWeb.Models
{
    public class UraniaWebDbContext : DbContext
    {
        public UraniaWebDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Administrador> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Administrador>().ToTable("Administrador");
            modelbuilder.Entity<Article>().ToTable("Article");
            modelbuilder.Entity<Slider>().ToTable("Slider");
        }
    }
}
