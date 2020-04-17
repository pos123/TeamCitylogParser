using System;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class StageExitEntry : IStageExitType
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public StageExitEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
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

        public uint StageNo => (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "start", dataService.Data(entry.LineNumber), 0);

        public uint StageCount => (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "end", dataService.Data(entry.LineNumber), 0);

        public bool Succeeded => valueExtractor.GetValueAsNumber(entry.EntryType, "exitCode", dataService.Data(entry.LineNumber), 1) == 0;
    }
}
