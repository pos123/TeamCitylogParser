namespace TeamCityLogParser
{
    public static class Constants
    {
        public const uint NoiseEntryType = 1;
        public const uint SolutionStartEntryType = 2;
        public const uint SolutionEndRebuildSucceededEntryType = 3;
        public const uint SolutionEndBuildSucceededEntryType = 4;
        public const uint SolutionEndBuildFailedEntryType = 5;
        public const uint ProjectDefinitionEntryType = 6;
        public const uint ProjectEntryType = 7;
        public const uint ProjectSummaryType = 8;
        public const uint ProjectEndBuildFailedType = 9;
        public const uint ProjectEndBuildSucceededType = 10;
        public const uint ProjectEnd = 11;
        public const uint ProjectEmpty = 12;
        
        public static readonly string None = @"None";
    }
}