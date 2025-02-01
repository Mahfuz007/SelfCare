namespace Persistence.Repositories
{
    public static class UtilityService
    {
        public static DateTime GetStartOfDayUtc(DateTime date)
        {
            return date.ToUniversalTime().AddHours(-6);
        }

        public static DateTime GetEndOfDayUtc(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 17, 59, 59, 999, DateTimeKind.Utc);
        }
    }
}
