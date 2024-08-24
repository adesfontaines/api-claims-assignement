using Microsoft.EntityFrameworkCore;
namespace Api.Claims.Models
{
    public class ClaimsDBContext : DbContext 
    {
        public ClaimsDBContext(DbContextOptions<ClaimsDBContext> options) : base(options)
        {
        }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>().ToTable("claims"); // Lowercase table name
            modelBuilder.Entity<Claim>().Property(c => c.Id).HasColumnName("id"); // Lowercase column name
            modelBuilder.Entity<Claim>().Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Claim>().Property(c => c.Verified).HasColumnName("verified");
        }
    }
}