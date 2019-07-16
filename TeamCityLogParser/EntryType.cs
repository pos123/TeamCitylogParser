using System.Collections.Generic;

namespace TeamCityLogParser
{
    public class EntryType
    {
        public readonly uint id;

        private static readonly Dictionary<uint, EntryType> entryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.NoiseEntryType, new EntryType(Constants.NoiseEntryType)},
            {Constants.SolutionStartEntryType, new EntryType(Constants.SolutionStartEntryType)},
            {Constants.SolutionEndRebuildSucceededEntryType, new EntryType(Constants.SolutionEndRebuildSucceededEntryType)},
            {Constants.SolutionEndBuildSucceededEntryType, new EntryType(Constants.SolutionEndBuildSucceededEntryType)},
            {Constants.SolutionEndBuildFailedEntryType, new EntryType(Constants.SolutionEndBuildFailedEntryType)},
            {Constants.ProjectDefinitionEntryType, new EntryType(Constants.ProjectDefinitionEntryType)},
            {Constants.ProjectEntryType, new EntryType(Constants.ProjectEntryType)},
            {Constants.ProjectEmpty, new EntryType(Constants.ProjectEmpty)},
            {Constants.ProjectSummaryType, new EntryType(Constants.ProjectSummaryType)}
        };
        
        private EntryType(uint id)
        {
            this.id = id;
        }
        
        public static EntryType Noise()  { return entryTypes[Constants.NoiseEntryType]; }
        public static EntryType SolutionStart()  { return entryTypes[Constants.SolutionStartEntryType]; }
        public static EntryType SolutionEndRebuildSucceeded()  { return entryTypes[Constants.SolutionEndRebuildSucceededEntryType]; }
        public static EntryType SolutionEndBuildSucceeded()  { return entryTypes[Constants.SolutionEndBuildSucceededEntryType]; }
        public static EntryType SolutionEndBuildFailed()  { return entryTypes[Constants.SolutionEndBuildFailedEntryType]; }
        public static EntryType ProjectDefinition()  { return entryTypes[Constants.ProjectDefinitionEntryType]; }
        public static EntryType ProjectEntry()  { return entryTypes[Constants.ProjectEntryType]; }
        public static EntryType ProjectEmpty()  { return entryTypes[Constants.ProjectEmpty]; }
        public static EntryType ProjectSummary()  { return entryTypes[Constants.ProjectSummaryType]; }
        
    }
}
