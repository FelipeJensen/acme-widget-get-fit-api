namespace AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;

public class ActivitySignUpFiltered
{
    public long Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int? YearsOfExperience { get; }
    public string? Comments { get; }

    public ActivitySignUpFiltered(long id, string firstName, string lastName, int? yearsOfExperience, string? comments)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        YearsOfExperience = yearsOfExperience;
        Comments = comments;
    }
}