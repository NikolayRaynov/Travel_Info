using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;

namespace Travel_Info.Data.Configuration
{
    public class PlaceToVisitConfiguration : IEntityTypeConfiguration<PlaceToVisit>
    {
        public void Configure(EntityTypeBuilder<PlaceToVisit> builder)
        {
            builder.HasData(SeedDesiredPlaces());
        }

        private List<PlaceToVisit> SeedDesiredPlaces()
        {
            List<PlaceToVisit> desiredPlaces = new List<PlaceToVisit>()
            {
                new PlaceToVisit
                {
                    Id = 1,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                },
                new PlaceToVisit
                {
                    Id = 2,
                    UserId = "8b3d2e65-4498-4d45-9127-2fde83fef2a4"
                }
            };

            return desiredPlaces;
        }
    }
}
