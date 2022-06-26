using AcmeWidget.GetFit.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data;

public class GetFitDbContext : DbContext
{
 #pragma warning disable CS8618
    public GetFitDbContext()
    {
    }
 #pragma warning restore CS8618

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

 #pragma warning disable CS8618
    public GetFitDbContext(DbContextOptions<GetFitDbContext> options) : base(options)
    {
    }
 #pragma warning restore CS8618

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ActivityDate>().ToTable("ActivityDates");
    }

    public DbSet<Activity> Activities { get; set; }
}