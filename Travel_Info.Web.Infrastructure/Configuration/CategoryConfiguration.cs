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
                   Name = "Beach"
                },
                new Category()
                {
                   Id = 2,
                    Name = "Mount"
                },
                new Category()
                {
                   Id = 3,
                   Name = "Stroll"
                },
                new Category()
                {
                   Id = 4,
                   Name = "Historical"
                }
            };

            return categories;
        }
    }
}
