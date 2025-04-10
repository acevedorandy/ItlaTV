using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Repositories.dbo;
using ItlaTV.Persistance.Validations.dbo;
using Microsoft.Extensions.DependencyInjection;

namespace ItlaTV.IOC.Dependencies.dbo
{
    public static class DboDependency
    {
        public static void AddDboDependency(this IServiceCollection services)
        {
            services.AddScoped<IGenerosRepository, GenerosRepository>();
            services.AddScoped<IProductorasRepository, ProductorasRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();

            services.AddScoped<GenerosValidation>();
            services.AddScoped<ProductorasValidation>();
            services.AddScoped<SeriesValidation>();
        }
    }
}
