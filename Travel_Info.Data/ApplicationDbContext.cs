using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Configuration;
using Travel_Info.Data.Models;

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
        public DbSet<FavoritePlace> FavoritePlaces { get; set; } = null!;
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
        }
    }
}
