using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AcmeWidget.GetFit.Domain.ResultHandling;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data;

public class GetFitDbContext : DbContext
{
 #pragma warning disable CS8618
    public GetFitDbContext(DbContextOptions<GetFitDbContext> options) : base(options)
    {
    }
 #pragma warning restore CS8618

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityDate> ActivityDates { get; set; }
    public DbSet<ActivitySignUp> ActivitySignUps { get; set; }
}