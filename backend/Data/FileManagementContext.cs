using Microsoft.EntityFrameworkCore;
using FileManagerBackend.Models;
using File = FileManagerBackend.Models.File;

namespace FileManagerBackend.Services
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
            modelBuilder.Entity<TagAssignment>()
                .HasKey(ta => new { ta.FileId, ta.TagId });

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

