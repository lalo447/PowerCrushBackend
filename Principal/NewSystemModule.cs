using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewSystem.App;
using NewSystem.Data;

namespace NewSystem
{
    /// <summary>Configuration to assign the database connection.</summary>
    public static class NewSystemModule
    {
        public static IServiceCollection AddNewSystemModule(
         this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<NewSystemContext>(opt =>
            {
                opt.UseSqlite(conn, sql =>
                {
                    sql.MigrationsAssembly(typeof(NewSystemContext).Assembly.FullName);
                });
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(NewSystemModule).Assembly);
            });

            return services;
        }
    }

    public record Result<T>;
    public record Ok<T>(T Value) : Result<T>;
    public record Error<T>(string Code, string Message) : Result<T>;
}
