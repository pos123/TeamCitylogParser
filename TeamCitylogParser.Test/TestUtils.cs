using System;
using System.IO;


namespace TeamCityLogParser.Test
{
    public static class TestUtils
    {
        public static string GetTestFileContents(string fileName)
        {
            var testDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var testFile = Path.Combine(testDirectory, "test_files", fileName);
            if (File.Exists(testFile))
            {
                return File.ReadAllText(testFile);
            }
            return String.Empty;
        }
        
    }

}


/*
 # How to use powershell to extract code.
Clear-Host
Add-Type -Path C:\Users\Nadeem\source\repos\TeamCityLogParser.dll
$dataService = New-Object TeamCityLogParser.DataService(" [10:54:44] :    [exec] 158>------ Build started: Project: blah x x x, Configuration: Release Win32 ------")
$valueExtractor = New-Object TeamCityLogParser.Extractors.ValueExtractor(New-Object TeamCityLogParser.DataDictionary)
$parser = New-Object TeamCityLogParser.CodeParser($dataService, $valueExtractor)
$parallelExecution = New-Object TeamCityLogParser.ParserParallelExecution($parser)
$parallelExecution.Run().GetAwaiter().GetResult()
$projectDefinitions = $parser.ProjectDefinitions
$projectDefinitions | Format-Table -AutoSize
 */
