using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.Parsers;
using Xunit;


namespace TeamCityLogParser.Test
{
    public class ParserTest
    {
        [Fact]
        public async Task GivenDataSourceForCode_ShouldParseAllEntryItems()
        {
            var data =
                "[10:54:44] :    [exec] 158>------ Build started: Project: blah x x x, Configuration: Release Win32 ------" + Environment.NewLine +
                "[10:54:44] :    [exec] Build Acceleration Console 8.0.1 (build 1867)" + Environment.NewLine +
                "[10:54:44] :    [exec] ========== Build: 35 succeeded, 0 failed, 5 up-to-date, 326 skipped ==========" + Environment.NewLine +
                "[10:54:44] :    [exec] ========== Rebuild All: 35 succeeded, 0 failed, 326 skipped ==========" + Environment.NewLine +
                "[10:54:44]W:    [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)" + Environment.NewLine +
                "[10:53:29] :    [exec] 44> error : blah blah blah" + Environment.NewLine +
                "[10:53:29] :    [exec] 44>" + Environment.NewLine +
                "[19:07:17] :    [exec] 54>Time Elapsed 00:00:14.56" + Environment.NewLine +
                "[19:07:17] :    [exec] 27>Build FAILED." + Environment.NewLine +
                "[19:07:17] :    [exec] 27>Build succeeded." + Environment.NewLine +
                "bunch of noise";
                
            var dataService = new DataService(data);
            var valueExtractor = new ValueExtractor(new DataDictionary());
            var parser = new CodeParser(dataService, valueExtractor);

            await parser.Parse(1, 11, (update) =>
            {
            });

            var noise = parser.Noise;
            var solutionStart = parser.SolutionStart;
            var solutionBuildSucceeded = parser.SolutionBuildSucceeded;
            var solutionRebuildSucceeded = parser.SolutionRebuildSucceeded;
            var projectDefinitions = parser.ProjectDefinitions;
            var projectEmptyEntries = parser.ProjectEmptyEntries;
            var projectBuildFailedEntries = parser.ProjectBuildFailedEntries;
            var projectBuildSucceededEntries = parser.ProjectBuildSucceededEntries;
            var projectEntries = parser.ProjectEntries;
            var projectEnd = parser.ProjectEndEntries;
        }


        [Fact]
        public async Task GivenDataSourceForGroups_ShouldParseAllGroupItems()
        {
            var logParser = new BuildLogParser(TestUtils.GetTestFileContents("test_2.txt"));
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 3);
            
            Assert.True(logParser.StageGroups[0].GroupStageNo == 1);
            Assert.False(logParser.StageGroups[0].IsStageFailure);
            Assert.True(logParser.StageGroups[0].IsStageSuccess);
            Assert.True(logParser.StageGroups[0].IsStageCompleted);
            Assert.True(logParser.StageGroups[0].StageLineRange.Item1 == 1);
            Assert.True(logParser.StageGroups[0].StageLineRange.Item2 == 2);
            Assert.True(logParser.StageGroups[0].StageGroupType == StageGroupType.SvnUpdate);

            Assert.True(logParser.StageGroups[1].GroupStageNo == 2);
            Assert.False(logParser.StageGroups[1].IsStageFailure);
            Assert.True(logParser.StageGroups[1].IsStageSuccess);
            Assert.True(logParser.StageGroups[1].IsStageCompleted);
            Assert.True(logParser.StageGroups[1].StageLineRange.Item1 == 3);
            Assert.True(logParser.StageGroups[1].StageLineRange.Item2 == 4);
            Assert.True(logParser.StageGroups[1].StageGroupType == StageGroupType.VerifyPackages);

            Assert.True(logParser.StageGroups[2].GroupStageNo == 3);
            Assert.False(logParser.StageGroups[2].IsStageFailure);
            Assert.True(logParser.StageGroups[2].IsStageSuccess);
            Assert.True(logParser.StageGroups[2].IsStageCompleted);
            Assert.True(logParser.StageGroups[2].StageLineRange.Item1 == 5);
            Assert.True(logParser.StageGroups[2].StageLineRange.Item2 == 6);
            Assert.True(logParser.StageGroups[2].StageGroupType == StageGroupType.CodeBuild);

            var (success, message) = logParser.GetStatement();
            Assert.True(message == "Solution build failed: 0 failed project(s), 0 error instance(s)");
            Assert.False(success);
        }

        [Fact]
        public async Task GivenDataSourceForGroupsWithOneFailure_ShouldParseFailedGroupItems()
        {
            var logParser = new BuildLogParser(TestUtils.GetTestFileContents("test_3.txt"));
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 1);

            Assert.True(logParser.StageGroups[0].GroupStageNo == 1);
            Assert.True(logParser.StageGroups[0].IsStageFailure);
            Assert.False(logParser.StageGroups[0].IsStageSuccess);
            Assert.True(logParser.StageGroups[0].IsStageCompleted);
            Assert.True(logParser.StageGroups[0].StageLineRange.Item1 == 1);
            Assert.True(logParser.StageGroups[0].StageLineRange.Item2 == 2);

            var (success, message) = logParser.GetStatement();
            Assert.True(message == "0 error entries(s) found");
            Assert.False(success);
        }

        [Fact]
        public async Task GivenDataSourceForGroupsWithOneSkippedAndTwoNot_ShouldParseGroupItems()
        {
            var logParser = new BuildLogParser(TestUtils.GetTestFileContents("test_4.txt"));
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 2);

            Assert.True(logParser.StageGroups[0].GroupStageNo == 2);
            Assert.False(logParser.StageGroups[0].IsStageFailure);
            Assert.True(logParser.StageGroups[0].IsStageSuccess);
            Assert.True(logParser.StageGroups[0].IsStageCompleted);
            Assert.True(logParser.StageGroups[0].StageGroupType == StageGroupType.VerifyPackages);

            Assert.True(logParser.StageGroups[1].GroupStageNo == 3);
            Assert.False(logParser.StageGroups[1].IsStageFailure);
            Assert.True(logParser.StageGroups[1].IsStageSuccess);
            Assert.True(logParser.StageGroups[1].IsStageCompleted);
            Assert.True(logParser.StageGroups[1].StageGroupType == StageGroupType.CodeBuild);

            var (success, message) = logParser.GetStatement();
            Assert.True(message == "Solution build failed: 0 failed project(s), 0 error instance(s)");
            Assert.False(success);
        }

        [Fact]
        public async Task GivenDataSourceWithFailedVerifyPackage_ShouldParseGroupItemsAndCodeParse()
        {
            var logParser = new BuildLogParser(TestUtils.GetTestFileContents("test_5.txt"));
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 1);

            Assert.True(logParser.StageGroups[0].GroupStageNo == 2);
            Assert.True(logParser.StageGroups[0].IsStageFailure);
            Assert.False(logParser.StageGroups[0].IsStageSuccess);
            Assert.True(logParser.StageGroups[0].IsStageCompleted);
            Assert.True(logParser.StageGroups[0].StageGroupType == StageGroupType.VerifyPackages);

            var (success, message) = logParser.GetStatement();
            Assert.True(message == "1 error entries(s) found");
            Assert.False(success);

            var results = logParser.VerifyPackageResults;
            Assert.NotNull(results);
            Assert.True(results.GetErrors().Count == 1);
            Assert.True(results.GetErrors()[0].Item1 == 2);
            Assert.True(results.GetErrors()[0].Item2 == " MSB4057: the target");

            var timeTaken = logParser.GetStageTimeTaken(StageGroupType.VerifyPackages);
            Assert.True(timeTaken?.Hours == 1);
        }

        [Fact]
        public async Task GivenBlankPayload_ShouldGiveCorrectBuildStatement()
        {
            var logParser = new BuildLogParser("nonsense");
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 0);
            var (success, message) = logParser.GetStatement();
            Assert.True(message == "no known build stages found in log");
            Assert.False(success);
        }

        [Fact]
        public async Task GivenBuildCodePayload_ShouldParseCorrectly()
        {
            var logParser = new BuildLogParser(TestUtils.GetTestFileContents("test_6.txt"));
            await logParser.Parse((notification) => { });

            Assert.True(logParser.StageGroups.Count == 1);
            var (success, message) = logParser.GetStatement();
            Assert.True(message == "Solution build failed: 3 failed project(s), 13 error instance(s)");
            Assert.False(success);

            var codeResults = logParser.CodeResults;
            Assert.True(codeResults.GetFailedProjectList().Count() == 3);
        }
    }
}
