using dealSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.Data;

public class DealContext : DbContext
{
    public DealContext(DbContextOptions<DealContext> options) : base(options) {}

    public DbSet<Deal> DealTable { get; set; } 
    public DbSet<Hotel> HotelTable { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deal>()
        .HasMany(d => d.Hotels)
        .WithOne(d => d.Deal)
        .HasForeignKey(h => h.DealId);

        modelBuilder.Entity<Hotel>()
        .HasOne(h => h.Deal)
        .WithMany(d => d.Hotels)
        .HasForeignKey(h => h.DealId );
    }

}