using System;
using System.Globalization;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class SolutionEndBuildBuildFailedEntry : ISolutionEndBuildFailedEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public SolutionEndBuildBuildFailedEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }
        
        public EntryType EntryType => entry.EntryType;
        
        public uint LineNumber => entry.LineNumber;
        
        public TimeSpan Time =>
            valueExtractor.GetValueAsTimeSpan(entry.EntryType, "time", @"hh\:mm\:ss",
                dataService.Data(entry.LineNumber), TimeSpan.Zero);

        public uint NonFatalErrors =>
            (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "buildFailedNonFatalErrors",
                dataService.Data(entry.LineNumber), 0);

        public uint Warnings =>
            (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "buildFailedWarnings",
                dataService.Data(entry.LineNumber), 0);

    }
}