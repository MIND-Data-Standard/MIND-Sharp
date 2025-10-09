using System;
namespace MindFrame.Core.Primitives
{
    public static class MindTimestampExtensions
    {
        private static readonly DateTimeOffset UnixEpoch = DateTimeOffset.UnixEpoch;
        public static DateTimeOffset ToDateTimeOffset(this MindTimestamp ts) => UnixEpoch.AddSeconds(ts.Value);
        public static MindTimestamp ToMindTimestamp(this DateTimeOffset dto)
        {
            double seconds = (dto - UnixEpoch).TotalSeconds;
            return new MindTimestamp((float)seconds);
        }
    }
}

