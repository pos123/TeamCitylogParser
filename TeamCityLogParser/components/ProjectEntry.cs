using System;
using System.Globalization;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;
using static System.String;

namespace TeamCityLogParser.components
{
    public class ProjectEntry : IProjectEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;
        
        public ProjectEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }

        public EntryType EntryType => entry.EntryType;
        
        public uint LineNumber  => entry.LineNumber;
        
        public TimeSpan Time =>
            valueExtractor.GetValueAsTimeSpan(entry.EntryType, "time", @"hh\:mm\:ss",
                dataService.Data(entry.LineNumber), TimeSpan.Zero);
        
        public uint ProjectId => 
            (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "projectId",
                dataService.Data(entry.LineNumber), 0);
        
        public string Data =>
            valueExtractor.GetValueAsString(entry.EntryType, "projectLineData",
                dataService.Data(entry.LineNumber), Empty).Trim();

    }
}