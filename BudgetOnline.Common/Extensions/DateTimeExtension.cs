namespace System
{
    public static class DateTimeExtension
    {
        public static DateTime FirstDateOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime? AsLocal(this DateTime? dt)
        {
            if (dt == null)
            {
                return null;
            }

            return dt.Value.AsLocal();
        }

        public static DateTime AsLocal(this DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Local)
            {
                return dt;
            }

            if (dt.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dt, DateTimeKind.Local);
            }

            return dt.ToLocalTime();
        }

        public static DateTime? AsUtc(this DateTime? dt)
        {
            if (dt == null)
            {
                return null;
            }

            return dt.Value.AsUtc();
        }

        public static DateTime AsUtc(this DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Utc)
            {
                return dt;
            }

            if (dt.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            }

            return dt.ToUniversalTime();
        }
    }
}
