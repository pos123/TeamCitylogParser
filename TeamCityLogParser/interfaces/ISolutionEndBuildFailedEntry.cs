using System;

namespace TeamCityLogParser.interfaces
{
    public interface ISolutionEndBuildFailedEntry : IEntry
    {
        uint NonFatalErrors { get;  }
        uint Warnings { get;  }
        TimeSpan Time { get; }
    }
}