using AcmeWidget.GetFit.Domain.ActivitySignups;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public class ActivitySignUpRepository : IActivitySignUpRepository
{
    private readonly IGetFitDbContext _context;

    public ActivitySignUpRepository(IGetFitDbContext context)
    {
        _context = context;
    }

    public bool Exists(string email, long activityId) => _context.Query<ActivitySignUp>().Any(p => p.Email == email && p.ActivityId == activityId);

    public Task<ActivitySignUp?> Get(long id) => _context.FindAsync<ActivitySignUp, long>(id);

    public Task Add(ActivitySignUp activitySignUp) => _context.AddAsync(activitySignUp);

    public Task Persist() => _context.SaveChangesAsync();
}