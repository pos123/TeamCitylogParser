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