using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using Xunit;


namespace TeamCityLogParser.Test
{
    public class ParserTest
    {
        [Fact]
        public void GivenDataSource_ShouldParseAllEntryItems()
        {
            var data =
                " [10:54:44] :    [exec] 158>------ Build started: Project: blah x x x, Configuration: Release Win32 ------" + Environment.NewLine +
                " [10:54:44] :    [exec] Build Acceleration Console 8.0.1 (build 1867)" + Environment.NewLine +
                " [10:54:44] :    [exec] ========== Build: 35 succeeded, 0 failed, 5 up-to-date, 326 skipped ==========" + Environment.NewLine +
                " [10:54:44] :    [exec] ========== Rebuild All: 35 succeeded, 0 failed, 326 skipped ==========" + Environment.NewLine +
                " [10:54:44]W:    [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)" + Environment.NewLine +
                " [10:53:29] :    [exec] 44> error : blah blah blah" + Environment.NewLine +
                " [10:53:29] :    [exec] 44>" + Environment.NewLine +
                " [19:07:17] :    [exec] 54>Time Elapsed 00:00:14.56" + Environment.NewLine +
                " [19:07:17] :    [exec] 27>Build FAILED." + Environment.NewLine +
                " [19:07:17] :    [exec] 27>Build succeeded." + Environment.NewLine +
                "bunch of noise";
                
            var dataService = new DataService(data);
            var valueExtractor = new ValueExtractor(new DataDictionary());
            var parser = new Parser(dataService, valueExtractor);

            var noise = parser.Noise;
            var solutionStart = parser.SolutionStart;
            var solutionFailedEntry = parser.SolutionFailedEntry;
            var solutionBuildSucceeded = parser.SolutionBuildSucceeded;
            var solutionRebuildSucceeded = parser.SolutionRebuildSucceeded;
            var projectDefinitions = parser.ProjectDefinitions;
            var projectEmptyEntries = parser.ProjectEmptyEntries;
            var projectBuildFailedEntries = parser.ProjectBuildFailedEntries;
            var projectBuildSucceededEntries = parser.ProjectBuildSucceededEntries;
            var projectEntries = parser.ProjectEntries;
            var projectEnd = parser.ProjectEnd;
        }

        [Fact]
        public async Task GivenTestFileName_ShouldParseAllEntryItems()
        {
            var sync = new object();
            
            var watch = new Stopwatch();
            watch.Start();
            
            var valueExtractor = new ValueExtractor(new DataDictionary());
            var dataService = new DataService(TestUtils.GetTestFileContents("test_1.txt"));
            var parser = new Parser(dataService, valueExtractor);

            var entryTypes = new List<uint>();

            var parserParallelExecution = new ParserParallelExecution(parser, (entryType) =>
            {
                lock (sync)
                {
                    entryTypes.Add(entryType);
                }
            });
            await parserParallelExecution.Run();

            entryTypes.Sort();
        }


    }
}
