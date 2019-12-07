using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class Entry : IEntry
    {
        public Entry(EntryType entryType, uint lineNumber)
        {
            EntryType = entryType;
            LineNumber = lineNumber;
        }

        public EntryType EntryType { get; }

        public uint LineNumber { get; }
    }
}