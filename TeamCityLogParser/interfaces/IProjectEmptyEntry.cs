using System;

namespace TeamCityLogParser.interfaces
{
    public interface IProjectEmptyEntry : IEntry
    {
        uint ProjectId { get; }
        TimeSpan Time { get; }
    }
}