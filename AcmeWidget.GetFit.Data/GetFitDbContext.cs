using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data;

public class GetFitDbContext : DbContext, IGetFitDbContext
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

    public IQueryable<T> Query<T>() where T : class => Set<T>().AsQueryable();

    public Task<T?> FindAsync<T, TK>(TK id) where T : class => base.FindAsync<T>(id).AsTask();

    public Task AddAsync<T>(T entity) => base.AddAsync(entity!).AsTask();

    public new void Remove<T>(T entity) => base.Remove(entity!);

    public Task SaveChangesAsync() => base.SaveChangesAsync();
}