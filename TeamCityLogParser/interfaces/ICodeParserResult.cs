using System;
using System.Collections.Generic;
using System.Text;
using TeamCityLogParser.Parsers;

namespace TeamCityLogParser.interfaces
{
    public interface ICodeParserResult
    {
        IEnumerable<uint> GetFailedProjectList();
        List<Tuple<uint, string, string, string, string>> GetBuildErrorsOutputForProject(uint projectId);
        List<Tuple<uint, string, string, string, string>> GetBuildErrorsOutput();
        IEnumerable<Tuple<uint, string>> GetProjectData(uint projectId);
        string GetSortedProjectData();
        List<ProjectLineError> GetProjectLineErrors();
        List<IProjectDefinitionEntry> GetProjectDefinitions();
        List<IProjectEntry> GetProjectEntries();
    }
}
