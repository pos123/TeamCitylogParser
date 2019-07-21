using System;

namespace TeamCityLogParser.interfaces
{
    public interface ISolutionEndRebuildSucceededEntry : IEntry, ISucceedFailedSkipped
    {
        TimeSpan Time { get; }
    }
}