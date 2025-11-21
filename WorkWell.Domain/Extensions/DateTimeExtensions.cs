namespace WorkWell.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateToMinutes(this DateTime dt)
            => new(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Kind);
    }
}