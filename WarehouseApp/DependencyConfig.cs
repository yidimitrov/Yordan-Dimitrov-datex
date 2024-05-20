using Microsoft.EntityFrameworkCore;
using WarehouseWebApi.Repository;
using WarehouseWebApi.Services;

namespace WarehouseWebApi
{
    public static class DependencyConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<WarehouseService>();
            services.AddScoped<DbRepository>();

            services.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseNpgsql("User ID=postgres;Password=localuser;Host=localhost;Port=5432;Database=warehouse;");
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
