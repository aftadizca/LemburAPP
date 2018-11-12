using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AplikasiLembur.Models
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<KaryawanModel> Karyawans { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<LemburModel> Lemburs { get; set; }
        public DbSet<LemburDetailsModel> LemburDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LemburDetailsModel>().HasKey(p => new { p.LemburId, p.KaryawanId });
        }
    }

}
  
