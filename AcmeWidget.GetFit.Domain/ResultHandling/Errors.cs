﻿namespace AcmeWidget.GetFit.Domain.ResultHandling;

public static class Errors
{
    public static class General
    {
        public const string EntityAlreadyExistsCode = "entity.already.exists";

        public static Error EntityAlreadyExists() => new(EntityAlreadyExistsCode);
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
            new(StartDateAfterEndDateCode, $"Start Date: \"{startDate}\" can not be after End Date: \"{endDate}\"");

        public static Error PeriodFrequencyWithoutEndDate() =>
            new(PeriodFrequencyWithoutEndDateCode, "Period frequency without end date");
    }
}