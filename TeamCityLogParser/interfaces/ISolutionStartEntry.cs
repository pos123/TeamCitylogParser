using System;

namespace TeamCityLogParser.interfaces
{
    public interface ISolutionStartEntry : IEntry
    {
        string SolutionStart { get; }
        TimeSpan Time { get; }
    }
}