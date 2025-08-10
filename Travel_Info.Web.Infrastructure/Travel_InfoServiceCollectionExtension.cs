using Microsoft.Extensions.DependencyInjection;
using Travel_Info.Data.Repository;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Web.Infrastructure
{
    public static class Travel_InfoServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IFavoritePlaceService, FavoritePlaceService>();
            services.AddScoped<IPlaceToVisitService, PlaceToVisitService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IFileService, FileService>();

			return services;
        }
    }
}
