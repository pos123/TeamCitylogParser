using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.Parsers;
using Xunit;



namespace TeamCitylogParser.Test
{
    public class GenerateTest
    {
        [Fact]
        public void Generate_file()
        {
            const uint projectCount = 256;
            const uint errorsPerProject = 2;
            const uint goodLinesPerProject = 500;

            const string projectBaseLabel = "project_label";
            const string testDirectory = @"D:\Downloads";

            var fileName = DateTime.Now.ToString("yyyy_dd_MMM_H_mm_ss") + ".log";

            using var file = new StreamWriter(Path.Combine(testDirectory, fileName));
            file.WriteLine("[00:00:00]W: Step 1/2: Nant");
            for (var i = 0; i < projectCount; ++i)
            {
                file.WriteLine($"[01:00:00] :    [exec] {i + 1}>------ Build started: Project: {projectBaseLabel}_{i + 1}, Configuration: Release Win32 ------");
                for (var j = 0; j < errorsPerProject; ++j)
                {
                    file.WriteLine($"[01:00:01] :    [exec] {i + 1}> error : D:\\Documents\\Source\\repos\\LongFactorials: error C2039: 'en': is not a member of 'std' ");
                }

                for (var j = 0; j < goodLinesPerProject; ++j)
                {
                    file.WriteLine($"[01:00:01] :    [exec] {i + 1}> compiling file_{j + 1}.cpp");
                }

                file.WriteLine($"[01:00:00] :    [exec] {i + 1}>Build FAILED.");
            }
            file.WriteLine("[10:10:10] :    [Step 1/2] Process exited with code 1");
        }
    }
}
