using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Info.Data.Models;

namespace Travel_Info.Web.Infrastructure.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
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
                   Name = "Море"
                },
                new Category()
                {
                   Id = 2,
                    Name = "Планина"
                },
                new Category()
                {
                   Id = 3,
                   Name = "Разходка"
                },
                new Category()
                {
                   Id = 4,
                   Name = "История"
                }
            };

            return categories;
        }
    }
}
