using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misp
{
    public static class Helper
    {
        public static DateTime TimeFromUnixTimestamp(int unixTimestamp)
        {
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }
        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }
    }

    public enum ThreatLevel : byte
    {
        Undefined = 0,
        Low = 1,
        Medium = 2,
        High = 3
    }
    public enum AnalysisLevel : byte
    {
        Initial = 0,
        Ongoing = 1,
        Complete = 2
    }
    public enum Distribution : byte
    {
        YourOrganizationOnly = 0,
        ThisCommunityOnly = 1,
        ConnectedCommunities = 2,
        AllCommunities = 3,
        SharingGroup = 4
    }
    
}
