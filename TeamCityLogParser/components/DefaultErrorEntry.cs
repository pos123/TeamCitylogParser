using System;
using System.Collections.Generic;
using System.Text;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class DefaultErrorEntry : IDefaultErrorEntry
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public DefaultErrorEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }

        public EntryType EntryType => entry.EntryType;

        public uint LineNumber => entry.LineNumber;

        public string Error => valueExtractor.GetValueAsString(entry.EntryType, "error",
            dataService.Data(entry.LineNumber), string.Empty);
    }
}
