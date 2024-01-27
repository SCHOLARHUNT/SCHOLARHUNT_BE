﻿using EDUHUNT_BE.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EDUHUNT_BE.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ScholarshipInfo> ScholarshipInfos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<QA> QAs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Sender).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Receiver).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.SentAt).IsRequired();

                // Additional configurations for Message entity can be added here if needed
            });

            modelBuilder.Entity<QA>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AskerId).IsRequired();
                entity.Property(e => e.AnswerId).IsRequired();
                entity.Property(e => e.Question).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Answer).HasMaxLength(255).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                // Additional configurations for QA entity can be added here if needed
            });

            // Additional configurations for other entities can be added here if needed
        }
    }
}
