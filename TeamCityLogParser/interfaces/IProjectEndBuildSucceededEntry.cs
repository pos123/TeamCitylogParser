namespace TeamCityLogParser.interfaces
{
    public interface IProjectEndBuildSucceededEntry : IEntry
    {
        uint Id { get; }
        string BuildSucceeded { get; }
    }
}