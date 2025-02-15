using Travel_Info.Data.Repository;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Services.Data.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
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

            return services;
        }
    }
}
