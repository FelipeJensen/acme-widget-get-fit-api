using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Domain.Activities;
using Bogus;

namespace AcmeWidget.GetFit.Tests.Domain;

public class FakeGenerator
{
    private readonly Faker _faker = new();

    public CreateActivityDate InvalidCreateActivityDate => new()
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(-1),
        Frequency = _faker.PickRandom<ActivityFrequency>()
    };

    public CreateActivityDate CreateActivityDate => new()
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(1),
        Frequency = _faker.PickRandom<ActivityFrequency>()
    };
}