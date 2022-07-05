using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public class ActivitySignUpRepository : IActivitySignUpRepository
{
    private readonly IGetFitDbContext _context;

    public ActivitySignUpRepository(IGetFitDbContext context)
    {
        _context = context;
    }

    public bool Exists(string email, long activityDateId) => _context.Query<ActivitySignUp>().Any(p => p.Email == email && p.ActivityDateId == activityDateId);

    public Task<ActivitySignUp?> Get(long id) => _context.FindAsync<ActivitySignUp, long>(id);

    public Task Add(ActivitySignUp activitySignUp) => _context.AddAsync(activitySignUp);

    public Task Persist() => _context.SaveChangesAsync();

    public async Task<List<Activity>> GetActivitiesWithSignUp(string? name, long? activityId, long? activityDateId)
    {
        var activities = await _context.Query<Activity>()
                                       .Include(p => p.ActivityDates)
                                       .Include(p => p.ActivitySignUps.Where(m => string.IsNullOrWhiteSpace(name) || m.FirstName.Contains(name) || m.LastName.Contains(name)))
                                       .Where(p => activityId == null || p.Id == activityId)
                                       .Where(p => activityDateId == null || p.ActivityDates.Any(k => k.Id == activityDateId))
                                       .Where(p => p.ActivitySignUps.Any(m => string.IsNullOrWhiteSpace(name) || m.FirstName.Contains(name) || m.LastName.Contains(name)))
                                       .ToListAsync();

        return activities;
    }
}