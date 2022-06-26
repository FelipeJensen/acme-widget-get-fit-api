using AcmeWidget.GetFit.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data.ActivityRepositories;

public class ActivityRepository : IActivityRepository
{
    private readonly GetFitDbContext _context;

    public ActivityRepository(GetFitDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Exists(string name)
    {
        return await _context.Activities.AnyAsync(p => p.Name == name);
    }

    public Task Add(Activity activity) => _context.AddAsync(activity).AsTask();

    public Task<Activity?> Get(long id) => _context.Activities.FindAsync(id).AsTask();

    public void Delete(Activity activity) => _context.Activities.Remove(activity);

    public IEnumerable<Lookup<long>> Lookup()
    {
        var lookups = _context.Activities
                              .AsNoTracking()
                              .Select(p => new { p.Id, p.Name })
                              .ToList()
                              .Select(p => new Lookup<long>(p.Id, p.Name));

        return lookups;
    }

    public IEnumerable<ActivityDate?> DatesLookup(long id)
    {
        var lookups = _context.ActivityDates
                              .AsNoTracking()
                              .Where(p => p.ActivityId == id)
                              .ToList();

        return lookups;
    }

    public Task Persist() => _context.SaveChangesAsync();
}