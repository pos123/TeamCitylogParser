namespace TeamCityLogParser
{
    public enum StageGroupType { Unknown, SvnUpdate, VerifyPackages, CodeBuild }

    public static class Constants
    {
        public const uint StageStartType = 1;
        public const uint StageExitType = 2;
        public const uint StageSkippedType = 3;
       
        public const uint NoiseEntryType = 10;
        public const uint SolutionStartEntryType = 11;
        public const uint SolutionEndRebuildSucceededEntryType = 12;
        public const uint SolutionEndBuildSucceededEntryType = 13;
        public const uint ProjectDefinitionEntryType = 14;
        public const uint ProjectEntryType = 15;
        public const uint ProjectEmptyEntryType = 16;
        public const uint ProjectEndEntryType = 17;
        public const uint ProjectEndBuildFailedEntryType = 18;
        public const uint ProjectEndBuildSucceededEntryType = 19;

        public const uint DefaultErrorEntryType = 30;

        public const string StageSvnUpdate = "svn update";
        public const string StageVerifyPackages = "verify packages";
        public const string StageNant = "nant";
    }
}