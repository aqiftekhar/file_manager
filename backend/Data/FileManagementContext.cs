using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Models;
using File = FileManagerBackend.Models.File;

namespace FileManagerBackend.Services.Data
{
    public class FileManagementContext : DbContext
    {
        public FileManagementContext(DbContextOptions<FileManagementContext> options) : base(options) { }

        public DbSet<Volume> Volumes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagAssignment> TagAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring primary keys and auto-increment
            modelBuilder.Entity<Volume>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<File>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Tag>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TagAssignment>()
                .HasKey(ta => new { ta.FileId, ta.TagId });

            // Configuring relationships
            modelBuilder.Entity<TagAssignment>()
                .HasOne(ta => ta.File)
                .WithMany(f => f.TagAssignments)
                .HasForeignKey(ta => ta.FileId);

            modelBuilder.Entity<TagAssignment>()
                .HasOne(ta => ta.Tag)
                .WithMany(t => t.TagAssignments)
                .HasForeignKey(ta => ta.TagId);
        }
    }
}

