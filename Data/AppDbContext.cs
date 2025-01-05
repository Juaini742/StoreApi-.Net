using BackendStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendStore.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) && !entityType.ClrType.IsAbstract)
                {
                    var entity = modelBuilder.Entity(entityType.ClrType);

                    entity.Property(nameof(BaseEntity.Id))
                          .IsRequired();

                    entity.Property(nameof(BaseEntity.CreatedAt))
                          .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                          .ValueGeneratedOnAdd();

                    entity.Property(nameof(BaseEntity.UpdatedAt))
                          .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                          .ValueGeneratedOnAddOrUpdate();
                }
            }
        }
    }
}
