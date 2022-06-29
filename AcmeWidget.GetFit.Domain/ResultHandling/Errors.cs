namespace AcmeWidget.GetFit.Domain.ResultHandling;

public static class Errors
{
    public static class General
    {
        public const string EntityAlreadyExistsCode = "entity.already.exists";
        public const string EntityNotFoundCode = "entity.not.found";
        public const string EnumNotDefinedCode = "enum.not.defined";
        public const string RequiredCode = "enum.not.defined";

        public static Error EntityAlreadyExists(string entity) => new(EntityAlreadyExistsCode, $"{entity} already exists.");

        public static Error NotFound(string entity) => new(EntityNotFoundCode, $"{entity} not found.");

        public static Error EnumNotDefined(int enumValue, string enumName)
            => new(EnumNotDefinedCode, $"Value '{enumValue}' not defined for enum of type '{enumName}'");

        public static Error Required(string field)
            => new(RequiredCode, $"{field} is required");
    }

    public static class Activity
    {
        public const string ActivityNameEmptyCode = "activity.name.empty";

        public static Error ActivityNameEmpty() =>
            new(ActivityNameEmptyCode, "Activity name must have value.");
    }

    public static class ActivityDate
    {
        public const string StartDateAfterEndDateCode = "activityDate.startDate.after.endDate";
        public const string PeriodFrequencyWithoutEndDateCode = "activityDate.frequency.without.endDate";

        public static Error StartDateAfterEndDate(DateTime startDate, DateTime endDate) =>
            new(StartDateAfterEndDateCode, $"Start Date: \"{startDate}\" can not be after End Date: \"{endDate}\".");

        public static Error PeriodFrequencyWithoutEndDate() =>
            new(PeriodFrequencyWithoutEndDateCode, "Period frequency without end date.");
    }

    public static class ActivitySignUp
    {
        public const string AlreadySignedUpCode = "activitySignUp.already.signed.up";

        public static Error AlreadySignedUp(string email, string activity) =>
            new(AlreadySignedUpCode, $"The email '{email}' has already signed up for the '{activity}' activity.");
    }
}