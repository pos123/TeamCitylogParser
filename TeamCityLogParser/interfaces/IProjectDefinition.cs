namespace TeamCityLogParser.interfaces
{
    public interface IProjectDefinition
    {
        string Name { get; }
        uint Id { get;}
        string Configuration { get; }    
    }
}