using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;

namespace Travel_Info.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                   Id = 1,
                   NameEn = "Beach", 
                   NameBg = "Плаж"
                },
                new Category()
                {
                   Id = 2,
                   NameEn = "Mount", 
                   NameBg = "Планина"
                },
                new Category()
                {
                   Id = 3,
                   NameEn = "Stroll", 
                   NameBg = "Разходка"
                },
                new Category()
                {
                   Id = 4,
                   NameEn = "Historical", 
                   NameBg = "Исторически"
                }
            };

            return categories;
        }
    }
}
