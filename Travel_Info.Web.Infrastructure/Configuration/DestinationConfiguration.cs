using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;
using static Travel_Info.Common.ConfigurationConstants.Destination;

namespace Travel_Info.Web.Infrastructure.Configuration
{
    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.HasData(SeedDestinations());

            builder
                .HasOne(u => u.User)
                .WithMany(d => d.Destinations)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private List<Destination> SeedDestinations()
        {
            var destinations = new List<Destination>
            {
                new Destination
                {
                    Id = 1,
                    Name = "Парк Росенец",
                    Description = beachDescription,
                    CategoryId = 1,
                    FavoritePlaceId = 1,
                    PlaceToVisitId = 1,
                },
                new Destination
                {
                    Id = 2,
                    Name = "Дявoлcĸoтo гъpлo",
                    Description = mountDescription,
                    CategoryId = 2,
                    FavoritePlaceId = 2,
                    PlaceToVisitId = 2,
                },
                new Destination
                {
                    Id = 3,
                    Name = "Екопътека Струилица",
                    Description = walkDescription,
                    CategoryId = 3,
                    FavoritePlaceId = 1,
                    PlaceToVisitId = 2,
                },
                new Destination
                {
                    Id = 4,
                    Name = "Царевец",
                    Description = historicalPlaceDescription,
                    CategoryId = 4,
                    FavoritePlaceId = 2,
                    PlaceToVisitId = 1,
                }
            };

            return destinations;
        }
    }
}
