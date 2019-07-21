using System;

namespace TeamCityLogParser.interfaces
{
    public interface IProjectEndBuildFailedEntry : IEntry
    {
        uint Id { get; }
        string BuildFailed { get; }   
        TimeSpan Time { get; }
    }
}