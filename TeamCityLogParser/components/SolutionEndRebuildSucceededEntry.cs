using System;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class SolutionEndRebuildSucceededEntry : ISolutionEndRebuildSucceededEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public SolutionEndRebuildSucceededEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }

        public EntryType EntryType => entry.EntryType;
        
        public uint LineNumber  => entry.LineNumber;
        
        public TimeSpan Time =>
            valueExtractor.GetValueAsTimeSpan(entry.EntryType, "time", @"hh\:mm\:ss",
                dataService.Data(entry.LineNumber), TimeSpan.Zero);

        public uint Succeeded =>
            (uint )valueExtractor.GetValueAsNumber(entry.EntryType, "buildSucceeded",
                dataService.Data(entry.LineNumber), 0);

        public uint Failed =>
            (uint )valueExtractor.GetValueAsNumber(entry.EntryType, "buildFailed",
                dataService.Data(entry.LineNumber), 0);
        
        public uint Skipped =>
            (uint )valueExtractor.GetValueAsNumber(entry.EntryType, "buildSkipped",
                dataService.Data(entry.LineNumber), 0);
    }
}