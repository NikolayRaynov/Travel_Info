using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Travel_Info.Data.Models;
using Travel_Info.Web.Infrastructure.Configuration;

namespace Travel_Info.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Destination> Destinations { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<PlaceToVisit> PlacesToVisit { get; set; } = null!;
        public DbSet<FavoritePlace> FavoritesPlaces { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FavoritePlaceConfiguration());
            builder.ApplyConfiguration(new PlaceToVisitConfiguration());
            builder.ApplyConfiguration(new DestinationConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());

            base.OnModelCreating(builder);

            GlobalFilter(builder);
        }

        private static void GlobalFilter(ModelBuilder builder)
        {
            builder.Entity<Destination>().HasQueryFilter(d => !d.IsDeleted);
            builder.Entity<Image>().HasQueryFilter(i => !i.IsDeleted);
            builder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
            builder.Entity<FavoritePlace>().HasQueryFilter(fp => !fp.IsDeleted);
            builder.Entity<PlaceToVisit>().HasQueryFilter(ptv => !ptv.IsDeleted);
        }
    }
}
