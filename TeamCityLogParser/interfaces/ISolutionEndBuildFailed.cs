namespace TeamCityLogParser.interfaces
{
    public interface ISolutionEndBuildFailed : IEntry
    {
        uint NonFatalErrors { get;  }
        uint Warnings { get;  }
    }
}