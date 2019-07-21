using System.Collections.Generic;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class ParserResult
    {
        private List<INoiseEntry> Noise { get; set; }
        private ISolutionStartEntry Solution { get; set; }
        private ISolutionEndBuildFailedEntry SolutionFailedEntry { get; set; }
        private ISolutionEndBuildSucceededEntry SolutionBuildSucceeded { get; set; }
        private ISolutionEndRebuildSucceededEntry SolutionRebuildSucceeded { get; set; }
        private List<IProjectDefinitionEntry> ProjectDefinitions { get; set; }
        private List<IProjectEmptyEntry> ProjectEmptyEntries { get; set; }
        private List<IProjectEndBuildFailedEntry> ProjectBuildFailedEntries { get; set; }
        private List<IProjectEndBuildSucceededEntry> ProjectBuildSucceededEntries { get; set; }
        private List<IProjectEntry> ProjectEntries { get; set; }
        private List<IProjectEndEntry> ProjectEnd { get; set; }
    }
}