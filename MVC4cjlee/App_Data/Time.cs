using System;

namespace MVC4cjlee.Utility
{
    /// <summary>
    /// Time의 요약 설명입니다.
    /// </summary>
    public class Time
    {
        #region MySql UnixTime 변환
        public static string ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long totalSeconds = (long)(datetime - sTime).TotalSeconds;
            return totalSeconds.ToString();
        }
        #endregion
    }
}