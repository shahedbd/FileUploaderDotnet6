using FileUploader.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AttachmentFile> AttachmentFile { get; set; }

    }
}
