using TeamCityLogParser.Extractors;
using Xunit;


namespace TeamCityLogParser.Test
{
    public class RegexExtractorTest
    {
        [Fact]
        public void GivenRegexProjectDefinition_ShouldProduceProjectDefinition()
        {
            var dataService = new DataService(" [10:54:44] :          [exec] 158>------ Build started: Project: StarWinForms x x x, Configuration: Release Win32 ------");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectDefinitionEntry = EntryFactory.CreateProjectDefinitionEntry(1, valueExtractor, dataService);
            
            Assert.Equal((uint)158, projectDefinitionEntry.Id);
            Assert.Equal(EntryType.ProjectDefinition(), projectDefinitionEntry.EntryType);
            Assert.Equal("Release Win32", projectDefinitionEntry.Configuration);
        }
        
        [Fact]
        public void GivenRegexSolutionStart_ShouldExtractSolutionStartDefinition()
        {
            var dataService = new DataService(" [10:54:44] :          [exec] Build Acceleration Console 8.0.1 (build 1867)");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionStartEntry = EntryFactory.CreateSolutionStartEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionStart(), solutionStartEntry.EntryType);
            Assert.Equal("Build Acceleration Console", solutionStartEntry.SolutionStart);
        }
        
        [Fact]
        public void GivenRegexSolutionEndBuildSucceeded_ShouldExtractSolutionBuildSucceededDefinition()
        {
            var dataService = new DataService(" [10:54:44] :          [exec] ========== Build: 35 succeeded, 10 failed, 5 up-to-date, 326 skipped ==========");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionBuildSucceededEntry = EntryFactory.CreateSolutionEndBuildSucceededEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndBuildSucceeded(), solutionBuildSucceededEntry.EntryType);
            Assert.Equal((uint)35, solutionBuildSucceededEntry.Succeeded);
            Assert.Equal((uint)10, solutionBuildSucceededEntry.Failed);
            Assert.Equal((uint)326, solutionBuildSucceededEntry.Skipped);
            Assert.Equal((uint)5, solutionBuildSucceededEntry.UpToDate);
        }
        
        [Fact]
        public void GivenRegexSolutionEndRebuildSucceeded_ShouldExtractSolutionRebuildSucceededDefinition()
        {
            var dataService = new DataService(" [10:54:44] :          [exec] ========== Rebuild All: 35 succeeded, 10 failed, 326 skipped ==========");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionRebuildSucceededEntry = EntryFactory.CreateSolutionEndRebuildSucceededEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndRebuildSucceeded(), solutionRebuildSucceededEntry.EntryType);
            Assert.Equal((uint)35, solutionRebuildSucceededEntry.Succeeded);
            Assert.Equal((uint)10, solutionRebuildSucceededEntry.Failed);
            Assert.Equal((uint)326, solutionRebuildSucceededEntry.Skipped);
        }
        
        [Fact]
        public void GivenRegexSolutionEndBuildFailed_ShouldExtractSolutionEndBuildFailedDefinition()
        {
            var dataService = new DataService(" [10:54:44]W:          [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionEndBuildFailed = EntryFactory.CreateSolutionEndBuildFailedEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndBuildFailed(), solutionEndBuildFailed.EntryType);
            Assert.Equal((uint)8, solutionEndBuildFailed.NonFatalErrors);
            Assert.Equal((uint)15, solutionEndBuildFailed.Warnings);
        }
        
        [Fact]
        public void GivenRegexProjectEntry_ShouldExtractProjectEntryDefinition()
        {
            var dataService = new DataService(" [10:53:29] :            [exec] 44>blah blah blah");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEntry = EntryFactory.CreateProjectEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEntry(), projectEntry.EntryType);
            Assert.Equal((uint)44, projectEntry.ProjectId);
            Assert.Equal("blah blah blah", projectEntry.Data);
        }
        
        [Fact]
        public void GivenRegexProjectEmptyEntry_ShouldExtractProjectEmptyEntryDefinition()
        {
            var dataService = new DataService(" [10:53:29] :            [exec] 44>");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEmptyEntry = EntryFactory.CreateProjectEmptyEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEmptyEntry(), projectEmptyEntry.EntryType);
            Assert.Equal((uint)44, projectEmptyEntry.ProjectId);
        }
        
        [Fact]
        public void GivenRegexProjectEndEntry_ShouldExtractProjectEndEntryDefinition()
        {
            var dataService = new DataService(" [19:07:17] :          [exec] 126>Time Elapsed 00:00:14.56");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndEntry = EntryFactory.CreateProjectEndEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEndEntry(), projectEndEntry.EntryType);
            Assert.Equal((uint)126, projectEndEntry.Id);
            Assert.Equal("00:00:14.56", projectEndEntry.TimeElapsed);
        }
        
        [Fact]
        public void GivenRegexProjectEndBuildFailedEntry_ShouldExtractProjectEndBuildFailedEntryDefinition()
        {
            var dataService = new DataService(" [19:07:17] :       [exec] 27>Build FAILED.");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndBuildFailed = EntryFactory.CreateProjectEndBuildFailedEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectBuildFailedEntry(), projectEndBuildFailed.EntryType);
            Assert.Equal((uint)27, projectEndBuildFailed.Id);
            Assert.Equal("Build FAILED.", projectEndBuildFailed.BuildFailed);
        }
        
        [Fact]
        public void GivenRegexProjectEndBuildSucceededEntry_ShouldExtractProjectEndBuildSucceededEntryDefinition()
        {
            var dataService = new DataService(" [19:07:17] :       [exec] 54>Build succeeded.");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndBuildSucceeded = EntryFactory.CreateProjectEndBuildSucceededEntry(1, valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectBuildSucceededEntry(), projectEndBuildSucceeded.EntryType);
            Assert.Equal((uint)54, projectEndBuildSucceeded.Id);
            Assert.Equal("Build succeeded.", projectEndBuildSucceeded.BuildSucceeded);
        }
    }
}
