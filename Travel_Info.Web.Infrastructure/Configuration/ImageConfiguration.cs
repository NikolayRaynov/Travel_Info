using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;

namespace Travel_Info.Web.Infrastructure.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(SeedImages());
        }

        private List<Image> SeedImages()
        {
            var images = new List<Image>
            {
                new Image
                {
                    Id = 1,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpzqgqIh5O8gA3lalctwh0VDhqphHdYRf1ow&s",
                    Description = "Парк Росенец",
                    DestinationId = 1
                },
                new Image
                {
                    Id = 2,
                    Url = "https://sunrisinglife.com/wp-content/uploads/2020/02/DSC00496.jpg",
                    Description = "Дявoлcĸoтo гъpлo",
                    DestinationId = 2
                },
                new Image
                {
                    Id = 3,
                    Url = "https://static.pochivka.bg/sights.bgstay.com/images/01/1565/54c568cb2fbea.jpg",
                    Description = "Екопътека Струилица",
                    DestinationId = 3
                },
                new Image
                {
                    Id = 4,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNiTp27Uafvlivnn89hGTJIirhYgLbYHhZbw&s",
                    Description = "Царевец",
                    DestinationId = 4
                }
            };

            return images;
        }
    }
}
