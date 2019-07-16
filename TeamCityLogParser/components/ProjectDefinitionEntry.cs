using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class ProjectDefinitionEntry : IProjectDefinitionEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;
        
        public ProjectDefinitionEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }

        public EntryType EntryType => entry.EntryType;
        
        public uint LineNumber  => entry.LineNumber;
        
        public string Name =>
            valueExtractor.GetValueAsString(entry.EntryType, "projectName",
                dataService.Data(entry.LineNumber), string.Empty);
        public uint Id =>
            (uint) valueExtractor.GetValueAsNumber(entry.EntryType, "projectId",
                dataService.Data(entry.LineNumber), 0);

        public string Configuration =>
            valueExtractor.GetValueAsString(entry.EntryType, "projectConfiguration",
                dataService.Data(entry.LineNumber), string.Empty);

    }
}