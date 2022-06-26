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

    public Task Persist() => _context.SaveChangesAsync();
}