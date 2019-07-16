namespace TeamCityLogParser.Extractors
{
    public interface IValueExtractor
    {
        string GetValueAsString(EntryType entryType, string name, string data, string defaultValue);
        int GetValueAsNumber(EntryType entryType, string name, string data, int defaultValue);
    }
}