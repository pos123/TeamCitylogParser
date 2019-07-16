using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class SolutionEndBuildBuildFailed : ISolutionEndBuildFailed
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public SolutionEndBuildBuildFailed(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }
        
        public EntryType EntryType => entry.EntryType;
        
        public uint LineNumber => entry.LineNumber;
        
        public uint NonFatalErrors =>
            (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "buildFailedNonFatalErrors",
                dataService.Data(entry.LineNumber), 0);

        public uint Warnings =>
            (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "buildFailedWarnings",
                dataService.Data(entry.LineNumber), 0);

    }
}