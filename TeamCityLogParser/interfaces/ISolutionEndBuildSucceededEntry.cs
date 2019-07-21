using System;

namespace TeamCityLogParser.interfaces
{
    public interface ISolutionEndBuildSucceededEntry : IEntry, ISucceedFailedSkipped
    {
        uint UpToDate { get; }
        TimeSpan Time { get; }
    }
}