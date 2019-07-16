namespace TeamCityLogParser.interfaces
{
    public interface IProjectEntry : IEntry
    {
        uint ProjectId { get; }
        string Data { get; }
    }
}