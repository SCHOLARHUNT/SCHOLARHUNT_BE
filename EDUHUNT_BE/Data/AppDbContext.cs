using EDUHUNT_BE.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EDUHUNT_BE.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScholarshipInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Budget).HasColumnType("DECIMAL(19,4)");
                entity.Property(e => e.Title).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Location).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.SchoolName).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.CategoryId);
                entity.Property(e => e.AuthorId);
                entity.Property(e => e.IsInSite);

                // Additional configurations can be added here based on your requirements
            });

            // Additional configurations for other entities can be added here if needed

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<EDUHUNT_BE.Model.ScholarshipInfo> ScholarshipInfo { get; set; }
    }



}
