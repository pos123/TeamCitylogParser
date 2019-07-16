using System.Collections.Generic;

namespace TeamCityLogParser
{
    public class EntryType
    {
        public readonly uint Id;

        private static readonly Dictionary<uint, EntryType> EntryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.NoiseEntryType, new EntryType(Constants.NoiseEntryType)},
            {Constants.SolutionStartEntryType, new EntryType(Constants.SolutionStartEntryType)},
            {Constants.SolutionEndRebuildSucceededEntryType, new EntryType(Constants.SolutionEndRebuildSucceededEntryType)},
            {Constants.SolutionEndBuildSucceededEntryType, new EntryType(Constants.SolutionEndBuildSucceededEntryType)},
            {Constants.SolutionEndBuildFailedEntryType, new EntryType(Constants.SolutionEndBuildFailedEntryType)},
            {Constants.ProjectDefinitionEntryType, new EntryType(Constants.ProjectDefinitionEntryType)},
            {Constants.ProjectEntryType, new EntryType(Constants.ProjectEntryType)},
            {Constants.ProjectEmptyEntryType, new EntryType(Constants.ProjectEmptyEntryType)},
            {Constants.ProjectEndEntryType, new EntryType(Constants.ProjectEndEntryType)},
            {Constants.ProjectEndBuildFailedEntryType, new EntryType(Constants.ProjectEndBuildFailedEntryType)},
            {Constants.ProjectEndBuildSucceededEntryType, new EntryType(Constants.ProjectEndBuildSucceededEntryType)}
        };
        
        private EntryType(uint id)
        {
            Id = id;
        }
        
        public static EntryType Noise()  { return EntryTypes[Constants.NoiseEntryType]; }
        public static EntryType SolutionStart()  { return EntryTypes[Constants.SolutionStartEntryType]; }
        public static EntryType SolutionEndRebuildSucceeded()  { return EntryTypes[Constants.SolutionEndRebuildSucceededEntryType]; }
        public static EntryType SolutionEndBuildSucceeded()  { return EntryTypes[Constants.SolutionEndBuildSucceededEntryType]; }
        public static EntryType SolutionEndBuildFailed()  { return EntryTypes[Constants.SolutionEndBuildFailedEntryType]; }
        public static EntryType ProjectDefinition()  { return EntryTypes[Constants.ProjectDefinitionEntryType]; }
        public static EntryType ProjectEntry()  { return EntryTypes[Constants.ProjectEntryType]; }
        public static EntryType ProjectEmptyEntry()  { return EntryTypes[Constants.ProjectEmptyEntryType]; }
        public static EntryType ProjectEndEntry()  { return EntryTypes[Constants.ProjectEndEntryType]; }
        public static EntryType ProjectBuildFailedEntry()  { return EntryTypes[Constants.ProjectEndBuildFailedEntryType]; }
        public static EntryType ProjectBuildSucceededEntry()  { return EntryTypes[Constants.ProjectEndBuildSucceededEntryType]; }
        
    }
}
