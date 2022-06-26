using AcmeWidget.GetFit.Data.ActivityRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeWidget.GetFit.Data;

public static class Startup
{
    public static void AddDataDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GetFitDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IActivityRepository, ActivityRepository>();
    }
}