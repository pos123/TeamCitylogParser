using System.Collections.Generic;

namespace TeamCityLogParser
{
    public class EntryType
    {
        public readonly uint Id;
        public readonly uint Priority;

        public static readonly EntryType NoiseEntryType = new EntryType(Constants.NoiseEntryType, 10);
        public static readonly EntryType DefaultErrorEntryType = new EntryType(Constants.DefaultErrorEntryType, 1);

        public static readonly Dictionary<uint, EntryType> StageEntryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.StageStartType, new EntryType(Constants.StageStartType, 1)},
            {Constants.StageExitType, new EntryType(Constants.StageExitType, 1)},
            {Constants.StageSkippedType, new EntryType(Constants.StageSkippedType, 1)},
        };

        public static readonly Dictionary<uint, EntryType> DefaultEntryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.DefaultErrorEntryType, DefaultErrorEntryType},
        };

        public static readonly Dictionary<uint, EntryType> CodeBuildParserEntryTypes = new Dictionary<uint, EntryType>
        {
            {Constants.SolutionStartEntryType, new EntryType(Constants.SolutionStartEntryType, 1)},
            {Constants.SolutionEndRebuildSucceededEntryType, new EntryType(Constants.SolutionEndRebuildSucceededEntryType, 1)},
            {Constants.SolutionEndBuildSucceededEntryType, new EntryType(Constants.SolutionEndBuildSucceededEntryType, 1)},
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

        public static EntryType StageStartType() { return StageEntryTypes[Constants.StageStartType]; }
        public static EntryType StageExitType() { return StageEntryTypes[Constants.StageExitType]; }
        public static EntryType StageSkippedType() { return StageEntryTypes[Constants.StageSkippedType]; }

        public static EntryType SolutionStart()  { return CodeBuildParserEntryTypes[Constants.SolutionStartEntryType]; }
        public static EntryType SolutionEndRebuildSucceeded()  { return CodeBuildParserEntryTypes[Constants.SolutionEndRebuildSucceededEntryType]; }
        public static EntryType SolutionEndBuildSucceeded()  { return CodeBuildParserEntryTypes[Constants.SolutionEndBuildSucceededEntryType]; }
        public static EntryType ProjectDefinition()  { return CodeBuildParserEntryTypes[Constants.ProjectDefinitionEntryType]; }
        public static EntryType ProjectEntry()  { return CodeBuildParserEntryTypes[Constants.ProjectEntryType]; }
        public static EntryType ProjectEmptyEntry()  { return CodeBuildParserEntryTypes[Constants.ProjectEmptyEntryType]; }
        public static EntryType ProjectEndEntry()  { return CodeBuildParserEntryTypes[Constants.ProjectEndEntryType]; }
        public static EntryType ProjectBuildFailedEntry()  { return CodeBuildParserEntryTypes[Constants.ProjectEndBuildFailedEntryType]; }
        public static EntryType ProjectBuildSucceededEntry()  { return CodeBuildParserEntryTypes[Constants.ProjectEndBuildSucceededEntryType]; }

        public static EntryType Noise() { return NoiseEntryType; }
        public static EntryType DefaultErrorEntry()  {  return DefaultErrorEntryType;  }
    }
}
