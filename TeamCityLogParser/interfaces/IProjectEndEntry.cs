using System;

namespace TeamCityLogParser.interfaces
{
    public interface IProjectEndEntry : IEntry
    {
        uint Id { get; }
        TimeSpan TimeElapsed { get; }
        TimeSpan Time { get; }
    }
}