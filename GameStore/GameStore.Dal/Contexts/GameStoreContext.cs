using GameStore.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Contexts
{
    [ExcludeFromCodeCoverage]
    public class GameStoreContext : DbContext
    {
        public GameStoreContext()
        {
        }

        public GameStoreContext(DbContextOptions<GameStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<GameGenre> GameGenres { get; set; }

        public DbSet<GamePlatformType> GamePlatformTypes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGenre>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Genres)
                .HasForeignKey(x => x.GameId);
            modelBuilder.Entity<GameGenre>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.GenreId);

            modelBuilder.Entity<GamePlatformType>()
                .HasOne(x => x.Game)
                .WithMany(x => x.PlatformTypes)
                .HasForeignKey(x => x.GameId);
            modelBuilder.Entity<GamePlatformType>()
                .HasOne(x => x.PlatformType)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.PlatformTypeId);

            modelBuilder.Entity<Game>().HasIndex(g => g.Key).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();
            modelBuilder.Entity<PlatformType>().HasIndex(g => g.Type).IsUnique();
            modelBuilder.Entity<Publisher>().HasIndex(p => p.CompanyName).IsUnique();

            modelBuilder.Entity<Game>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.SumWithDiscount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.SumWithoutDiscount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(p => p.TotalSum).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Publisher>()
                .Property(p => p.CompanyName)
                .HasMaxLength(40);

            DatabaseSeeder.Seed(modelBuilder);
        }
    }
}