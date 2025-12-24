using ExecuteOS.Server.Modules.Tasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecuteOS.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Description)
                .HasMaxLength(2000);

                entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();

                entity.Property(e => e.Priority)
                .IsRequired()
                .HasConversion<string>();

                entity.Property(e => e.EstimatedHours)
                .HasPrecision(10, 2);

                entity.Property(e => e.ActualHours)
                .HasPrecision(10, 2);

                entity.Property(e => e.CreatedAt)
                .IsRequired();

                entity.Property(e => e.UpdatedAt)
                .IsRequired();

                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.CreatedAt);
            });
        }
    }
}
