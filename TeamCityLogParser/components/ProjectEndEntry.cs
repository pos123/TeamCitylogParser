using System;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class ProjectEndEntry : IProjectEndEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;
        
        public ProjectEndEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
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

        public uint Id =>
            (uint) valueExtractor.GetValueAsNumber(entry.EntryType, "projectId",
                dataService.Data(entry.LineNumber), 0);
        
        public TimeSpan TimeElapsed => 
            valueExtractor.GetValueAsTimeSpan(entry.EntryType, "timeElapsed", @"hh\:mm\:ss\.ff",
                dataService.Data(entry.LineNumber), TimeSpan.Zero);
            
    }
}