using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using Microsoft.EntityFrameworkCore;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public class ActivitySignUpRepository : IActivitySignUpRepository
{
    private readonly GetFitDbContext _context;

    public ActivitySignUpRepository(GetFitDbContext context)
    {
        _context = context;
    }

    public Task<Activity?> GetActivity(long id)
    {
        return _context.Activities.FindAsync(id).AsTask();
    }

    public Task<bool> Exists(string email, long activityId)
    {
        return _context.ActivitySignUps.AnyAsync(p => p.Email == email && p.ActivityId == activityId);
    }

    public Task<ActivityDate?> GetActivityDate(long activityDateId)
    {
        return _context.ActivityDates.FindAsync(activityDateId).AsTask();
    }

    public Task Add(ActivitySignUp activitySignUp) => _context.ActivitySignUps.AddAsync(activitySignUp).AsTask();
    public Task Persist() => _context.SaveChangesAsync();
}