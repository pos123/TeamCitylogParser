using System;
using TeamCityLogParser.Extractors;
using Xunit;


namespace TeamCityLogParser.Test
{
    public class RegexExtractorTest
    {
        [Fact]
        public void GivenRegexProjectDefinition_ShouldProduceProjectDefinition()
        {
            var dataService = new DataService("[10:54:44] :          [exec] 158>------ Build started: Project: StarWinForms x x x, Configuration: Release Win32 ------");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectDefinitionEntry = EntryFactory.CreateProjectDefinitionEntryFunc(1)(valueExtractor, dataService);
            
            Assert.Equal((uint)158, projectDefinitionEntry.Id);
            Assert.Equal("StarWinForms x x x", projectDefinitionEntry.Name);
            Assert.Equal(EntryType.ProjectDefinition(), projectDefinitionEntry.EntryType);
            Assert.Equal("Release Win32", projectDefinitionEntry.Configuration);
            Assert.Equal(new TimeSpan(10, 54, 44), projectDefinitionEntry.Time );
        }
        
        [Fact]
        public void GivenRegexSolutionStart_ShouldExtractSolutionStartDefinition()
        {
            var dataService = new DataService("[10:54:44] :          [exec] Build Acceleration Console 8.0.1 (build 1867)");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionStartEntry = EntryFactory.CreateSolutionStartEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionStart(), solutionStartEntry.EntryType);
            Assert.Equal("Build Acceleration Console", solutionStartEntry.SolutionStart);
            Assert.Equal(new TimeSpan(10, 54, 44), solutionStartEntry.Time);
        }
        
        [Fact]
        public void GivenRegexSolutionEndBuildSucceeded_ShouldExtractSolutionBuildSucceededDefinition()
        {
            var dataService = new DataService("[10:54:44] :          [exec] ========== Build: 35 succeeded, 10 failed, 5 up-to-date, 326 skipped ==========");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionBuildSucceededEntry = EntryFactory.CreateSolutionEndBuildSucceededEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndBuildSucceeded(), solutionBuildSucceededEntry.EntryType);
            Assert.Equal((uint)35, solutionBuildSucceededEntry.Succeeded);
            Assert.Equal((uint)10, solutionBuildSucceededEntry.Failed);
            Assert.Equal((uint)326, solutionBuildSucceededEntry.Skipped);
            Assert.Equal((uint)5, solutionBuildSucceededEntry.UpToDate);
            Assert.Equal(new TimeSpan(10, 54, 44), solutionBuildSucceededEntry.Time);
        }
        
        [Fact]
        public void GivenRegexSolutionEndRebuildSucceeded_ShouldExtractSolutionRebuildSucceededDefinition()
        {
            var dataService = new DataService("[10:54:44] :          [exec] ========== Rebuild All: 35 succeeded, 10 failed, 326 skipped ==========");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionRebuildSucceededEntry = EntryFactory.CreateSolutionEndRebuildSucceededEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndRebuildSucceeded(), solutionRebuildSucceededEntry.EntryType);
            Assert.Equal((uint)35, solutionRebuildSucceededEntry.Succeeded);
            Assert.Equal((uint)10, solutionRebuildSucceededEntry.Failed);
            Assert.Equal((uint)326, solutionRebuildSucceededEntry.Skipped);
            Assert.Equal(new TimeSpan(10, 54, 44), solutionRebuildSucceededEntry.Time);
        }
        
        [Fact]
        public void GivenRegexSolutionEndBuildFailed_ShouldExtractSolutionEndBuildFailedDefinition()
        {
            var dataService = new DataService("[10:54:44]W:          [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var solutionEndBuildFailed = EntryFactory.CreateSolutionEndBuildFailedEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.SolutionEndBuildFailed(), solutionEndBuildFailed.EntryType);
            Assert.Equal((uint)8, solutionEndBuildFailed.NonFatalErrors);
            Assert.Equal((uint)15, solutionEndBuildFailed.Warnings);
            Assert.Equal(new TimeSpan(10, 54, 44), solutionEndBuildFailed.Time);
        }
        
        [Fact]
        public void GivenRegexProjectEntry_ShouldExtractProjectEntryDefinition()
        {
            var dataService = new DataService("[10:53:29] :            [exec] 44>  blah blah blah");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEntry = EntryFactory.CreateProjectEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEntry(), projectEntry.EntryType);
            Assert.Equal((uint)44, projectEntry.ProjectId);
            Assert.Equal("  blah blah blah", projectEntry.Data);
            Assert.Equal(new TimeSpan(10, 53, 29), projectEntry.Time);
        }
        
        [Fact]
        public void GivenRegexProjectEmptyEntry_ShouldExtractProjectEmptyEntryDefinition()
        {
            var dataService = new DataService("[10:53:29] :            [exec] 44>");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEmptyEntry = EntryFactory.CreateProjectEmptyEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEmptyEntry(), projectEmptyEntry.EntryType);
            Assert.Equal((uint)44, projectEmptyEntry.ProjectId);
            Assert.Equal(new TimeSpan(10, 53, 29), projectEmptyEntry.Time);
        }
        
        [Fact]
        public void GivenRegexProjectEndEntry_ShouldExtractProjectEndEntryDefinition()
        {
            var dataService = new DataService("[19:07:17] :          [exec] 126>Time Elapsed 00:00:14.56");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndEntry = EntryFactory.CreateProjectEndEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectEndEntry(), projectEndEntry.EntryType);
            Assert.Equal((uint)126, projectEndEntry.Id);
            Assert.Equal(new TimeSpan(0, 0, 0, 14, 560), projectEndEntry.TimeElapsed);
            Assert.Equal(new TimeSpan(19, 07, 17), projectEndEntry.Time);
        }
        
        [Fact]
        public void GivenRegexProjectEndBuildFailedEntry_ShouldExtractProjectEndBuildFailedEntryDefinition()
        {
            var dataService = new DataService("[19:07:17] :       [exec] 27>Build FAILED.");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndBuildFailed = EntryFactory.CreateProjectEndBuildFailedEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectBuildFailedEntry(), projectEndBuildFailed.EntryType);
            Assert.Equal((uint)27, projectEndBuildFailed.Id);
            Assert.Equal("Build FAILED.", projectEndBuildFailed.BuildFailed);
            Assert.Equal(new TimeSpan(19, 07, 17), projectEndBuildFailed.Time);
        }
        
        [Fact]
        public void GivenRegexProjectEndBuildSucceededEntry_ShouldExtractProjectEndBuildSucceededEntryDefinition()
        {
            var dataService = new DataService("[19:07:17] :       [exec] 54>Build succeeded.");
            var dataDictionary = new DataDictionary();
            var valueExtractor = new ValueExtractor(dataDictionary);
            var projectEndBuildSucceeded = EntryFactory.CreateProjectEndBuildSucceededEntryFunc(1)(valueExtractor, dataService);

            Assert.Equal(EntryType.ProjectBuildSucceededEntry(), projectEndBuildSucceeded.EntryType);
            Assert.Equal((uint)54, projectEndBuildSucceeded.Id);
            Assert.Equal("Build succeeded.", projectEndBuildSucceeded.BuildSucceeded);
            Assert.Equal(new TimeSpan(19, 07, 17), projectEndBuildSucceeded.Time);
        }
    }
}
