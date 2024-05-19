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

            //services.AddDbContext<WarehouseDbContext>(options =>
            //{
            //    options.UseSqlServer("Password=admin;Persist Security Info=True;User ID=sa;Data Source=localhost;Initial Catalog=warehouse");
            //});
            //.AddScoped<WarehouseDbContext>(provider => provider.GetService<WarehouseDbContext>());

            services.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseNpgsql("User ID=postgres;Password=localuser;Host=localhost;Port=5432;Database=warehouse;");
            });
            //.AddScoped<WarehouseDbContext>(provider => provider.GetService<WarehouseDbContext>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
