using System;

namespace MtApi5
{
    internal class Mt5TimeConverter
    {
        public static DateTime ConvertFromMtTime(int time)
        {
            return new DateTime(DateTime.UnixEpoch.Ticks + (time * 0x989680L));
        }

        public static DateTime ConvertFromMtTime(long time)
        {
            return new DateTime(DateTime.UnixEpoch.Ticks + (time * 0x989680L));
        }

        public static int ConvertToMtTime(DateTime? time)
        {
            var result = 0;
            if (time == null || time == DateTime.MinValue)
                return result;
            result = (int)((time.Value.Ticks - DateTime.UnixEpoch.Ticks) / 0x989680L);
            return result;
        }
    }
}
