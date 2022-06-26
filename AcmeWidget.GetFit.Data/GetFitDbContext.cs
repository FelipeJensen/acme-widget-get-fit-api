 
using AcmeWidget.GetFit.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data;

public class GetFitDbContext : DbContext
{
 #pragma warning disable CS8618
    public GetFitDbContext(DbContextOptions<GetFitDbContext> options) : base(options)
    {
    }
 #pragma warning restore CS8618

    public DbSet<Activity> Activities { get; set; }
}