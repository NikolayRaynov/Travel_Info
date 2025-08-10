using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;

namespace Travel_Info.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(SeedReviews());
        }

        private List<Review> SeedReviews()
        {
            var reviews = new List<Review>()
            {
                new Review
                {
                    Id = 1,
                    DestinationId = 1,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    Rating = 5,
                    Comment = "Страхотно място!", 
                    CreatedAt = DateTime.Now
                },
                new Review
                {
                    Id = 2,
                    DestinationId = 2,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    Rating = 5,
                    CreatedAt = DateTime.Now
                },
                new Review
                {
                    Id = 3,
                    DestinationId = 3,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    Rating = 5,
                    Comment = "Страхотно място!",
                    CreatedAt = DateTime.Now
                },
                new Review
                {
                    Id = 4,
                    DestinationId = 4,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    Rating = 5,
                    CreatedAt = DateTime.Now
                }
            };

            return reviews;
        }
    }
}
