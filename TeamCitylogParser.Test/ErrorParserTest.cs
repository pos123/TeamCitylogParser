using System.Collections.Generic;
using System.Text;
using TeamCityLogParser;
using TeamCityLogParser.Extractors;
using Xunit;
using static System.String;

namespace TeamCitylogParser.Test
{
    public class ErrorParserTest
    {
        [Fact]
        public void GivenProjectEntryWithErrors_ShouldFindCorrectTypeOfError()
        {
            Assert.Equal("ERROR", ErrorParser.GetCategory(" here we have an error "));
            Assert.Equal("BSCMAKE", ErrorParser.GetCategory(" BK1599: "));
            Assert.Equal(Empty, ErrorParser.GetCategory(" BK15996: "));
            Assert.Equal("CMDLINE", ErrorParser.GetCategory("D8025 "));
            Assert.Equal("CMDLINE", ErrorParser.GetCategory("   D9025"));
            Assert.Equal("COMPILER", ErrorParser.GetCategory("   C999"));
            Assert.Equal("COMPILER", ErrorParser.GetCategory("   C9999:"));
            Assert.Equal(Empty, ErrorParser.GetCategory("   C99999 "));
            Assert.Equal("RUNTIME", ErrorParser.GetCategory("   R6023: "));
            Assert.Equal("CVTRES", ErrorParser.GetCategory("   CVT1105: "));
            Assert.Equal("EXPRESSION", ErrorParser.GetCategory("   CXX0015 "));
            Assert.Equal("LINKER", ErrorParser.GetCategory("   LNK1236: "));
            Assert.Equal("MATH", ErrorParser.GetCategory("   M1234: "));
            Assert.Equal("NMAKE", ErrorParser.GetCategory("   U1234: "));
            Assert.Equal("PROFILE_GUIDED", ErrorParser.GetCategory("   PG1236: "));
            Assert.Equal("PROJECT", ErrorParser.GetCategory("   PRJ1234: "));
            Assert.Equal("RESOURCE", ErrorParser.GetCategory("   RC1234: "));
            Assert.Equal("RESOURCE", ErrorParser.GetCategory("   RW1234: "));
            Assert.Equal(Empty, ErrorParser.GetCategory("   RS1234 "));
            Assert.Equal("CSHARP", ErrorParser.GetCategory("CS1236 "));
            Assert.Equal(Empty, ErrorParser.GetCategory(" blah blah "));
        }
    }
}
