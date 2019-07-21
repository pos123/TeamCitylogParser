using System;
using TeamCityLogParser.Extractors;
using Xunit;


namespace TeamCityLogParser.Test
{
    public class ParserTest
    {
        
        [Fact]
        public void GivenDataSource_ShouldParseAllNoiseEntryItems()
        {
            var parser = new Parser(" [10:54:44] :          [exec] Build Acceleration Console 8.0.1 (build 1867)" + Environment.NewLine + "sdsdssdsdssd");
            parser.Run();
     
        }

        
    }
}