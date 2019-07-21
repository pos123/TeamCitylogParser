using System.Collections.Generic;

namespace TeamCityLogParser
{
    public class EntryType
    {
        public readonly uint Id;
        public readonly uint Priority;
        
        public static readonly Dictionary<uint, EntryType> EntryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.NoiseEntryType, new EntryType(Constants.NoiseEntryType, 3)},
            {Constants.SolutionStartEntryType, new EntryType(Constants.SolutionStartEntryType, 1)},
            {Constants.SolutionEndRebuildSucceededEntryType, new EntryType(Constants.SolutionEndRebuildSucceededEntryType, 1)},
            {Constants.SolutionEndBuildSucceededEntryType, new EntryType(Constants.SolutionEndBuildSucceededEntryType, 1)},
            {Constants.SolutionEndBuildFailedEntryType, new EntryType(Constants.SolutionEndBuildFailedEntryType, 1)},
            {Constants.ProjectDefinitionEntryType, new EntryType(Constants.ProjectDefinitionEntryType, 1)},
            {Constants.ProjectEntryType, new EntryType(Constants.ProjectEntryType, 2)},
            {Constants.ProjectEmptyEntryType, new EntryType(Constants.ProjectEmptyEntryType, 1)},
            {Constants.ProjectEndEntryType, new EntryType(Constants.ProjectEndEntryType, 1)},
            {Constants.ProjectEndBuildFailedEntryType, new EntryType(Constants.ProjectEndBuildFailedEntryType, 1)},
            {Constants.ProjectEndBuildSucceededEntryType, new EntryType(Constants.ProjectEndBuildSucceededEntryType, 1)}
        };
        
        private EntryType(uint id, uint priority)
        {
            Id = id;
            Priority = priority;
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
