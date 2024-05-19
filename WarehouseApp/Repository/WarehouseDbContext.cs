using Microsoft.EntityFrameworkCore;
using WarehouseWebApi.Models.Db.Entity;

namespace WarehouseWebApi.Repository
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<BoxEntity> Boxes { get; set; }

        public DbSet<PalletEntity> Pallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalletEntity>()
                .HasMany(e => e.Boxes)
                .WithOne(e => e.Pallet)
                .HasForeignKey(e => e.PalletId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired(true);

            modelBuilder.Entity<BoxEntity>()
                .HasMany(e => e.Boxes)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired(false);
        }
    }
}
