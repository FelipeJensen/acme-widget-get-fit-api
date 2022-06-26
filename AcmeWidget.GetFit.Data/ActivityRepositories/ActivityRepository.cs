using AcmeWidget.GetFit.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data.ActivityRepositories;

public class ActivityRepository : IActivityRepository
{
    private readonly IGetFitDbContext _context;

    public ActivityRepository(IGetFitDbContext context)
    {
        _context = context;
    }

    public bool Exists(string name) => _context.Query<Activity>().Any(p => p.Name == name);
    public Task<Activity?> Get(long id) => _context.FindAsync<Activity, long>(id);
    public Task Add(Activity activity) => _context.AddAsync(activity);
    public void Delete(Activity activity) => _context.Remove(activity);
    public Task Persist() => _context.SaveChangesAsync();

    public IEnumerable<Lookup<long>> Lookup()
    {
        var lookups = _context.Query<Activity>()
                              .AsNoTracking()
                              .Select(p => new Lookup<long>(p.Id, p.Name));

        return lookups;
    }

    public IEnumerable<ActivityDate?> ActivityDates(long id)
    {
        var lookups = _context.Query<ActivityDate>()
                              .AsNoTracking()
                              .Where(p => p.ActivityId == id)
                              .ToList();

        return lookups;
    }
}