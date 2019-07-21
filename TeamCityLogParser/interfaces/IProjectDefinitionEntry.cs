using System;

namespace TeamCityLogParser.interfaces
{
    public interface IProjectDefinitionEntry : IEntry, IProjectDefinition 
    {
        TimeSpan Time { get; }
    }
}