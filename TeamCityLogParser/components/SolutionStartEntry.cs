using System;
using System.Globalization;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class SolutionStartEntry : ISolutionStartEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;
        
        public SolutionStartEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
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
        
        public string SolutionStart =>
            valueExtractor.GetValueAsString(entry.EntryType, "buildAccelerationConsole",
                dataService.Data(entry.LineNumber), string.Empty);
    }
}