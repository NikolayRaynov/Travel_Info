using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Info.Data.Models;

namespace Travel_Info.Web.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<ApplicationUser> SeedUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "user@mail.com",
                NormalizedUserName = "USER@MAIL.COM",
                Email = "user@mail.com",
                NormalizedEmail = "USER@MAIL.COM"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "user123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "Admin1234");

            users.Add(user);

            return users;
        }
    }
}
