using System;

namespace BashBook.Utility
{
    public static class UnixTimeBaseClass
    {
        public static long UnixDateNow
        {
            get
            {
                var date = (Int64)(DateTime.UtcNow.Date - new DateTime(1970, 1, 1)).TotalSeconds;
                return date;
            }
        }

        public static long UnixTimeNow
        {
            get
            {
                var date = (Int64)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                return date;
            }
        }

        public static string GetDateString(long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
            return dtDateTime.ToLongDateString();
        }
    }
}
