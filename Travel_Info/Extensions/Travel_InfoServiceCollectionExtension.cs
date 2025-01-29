using Travel_Info.Data.Repository;
using Travel_Info.Data.Repository.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Travel_InfoServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
