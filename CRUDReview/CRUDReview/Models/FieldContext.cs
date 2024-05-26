using Microsoft.EntityFrameworkCore;

namespace CRUDReview.Models
{
    public class FieldContext: DbContext
    {
        public FieldContext(DbContextOptions<FieldContext>options):base(options) 
        {
            
        }
        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            });
        }
    }
}
