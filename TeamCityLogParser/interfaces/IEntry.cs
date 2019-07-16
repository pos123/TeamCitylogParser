namespace TeamCityLogParser.interfaces
{
    public interface IEntry
    {
        EntryType EntryType { get; }
        uint LineNumber { get; }
    }
}