namespace TeamCityLogParser.interfaces
{
    public interface IProjectEndEntry : IEntry
    {
        uint Id { get; }
        string TimeElapsed { get; }
    }
}