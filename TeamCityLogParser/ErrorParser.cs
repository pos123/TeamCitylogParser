using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TeamCityLogParser
{
    public static class ErrorParser
    {
        //  https://docs.microsoft.com/en-us/cpp/error-messages/compiler-errors-1/c-cpp-build-errors?view=vs-2019
        private static Dictionary<string, Regex> vcErrors = new Dictionary<string, Regex>()
        {
            { "BSCMAKE", new Regex( @"\bBK(1|4)5\d\d(\b|:)", RegexOptions.Compiled) },
            { "CMDLINE", new Regex( @"\bD(8|9)0\d\d(\b|:)", RegexOptions.Compiled) },
            { "COMPILER", new Regex( @"\bC(\d{3}|\d{4})(\b|:)", RegexOptions.Compiled) },
            { "RUNTIME", new Regex( @"\bR60\d\d(\b|:)", RegexOptions.Compiled) },
            { "CVTRES", new Regex( @"\bCVT11\d\d(\b|:)", RegexOptions.Compiled) },
            { "EXPRESSION", new Regex( @"\bCXX00\d\d(\b|:)", RegexOptions.Compiled) },
            { "LINKER", new Regex( @"\bLNK\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "MATH", new Regex( @"\bM\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "NMAKE", new Regex( @"\bU\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "PROFILE_GUIDED", new Regex( @"\bPG\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "PROJECT", new Regex( @"\bPRJ\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "RESOURCE", new Regex( @"\bR(C|W)\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "CSHARP", new Regex( @"\bCS\d\d\d\d(\b|:)", RegexOptions.Compiled) },
            { "ERROR", new Regex( @"\berror\b", RegexOptions.Compiled) },
        };
        
        
        public static string GetCategory(string data)
        {
            foreach (var errorParser in from errorParser in vcErrors let match = errorParser.Value.Match(data) where match.Success select errorParser)
            {
                return errorParser.Key;
            }
            return string.Empty;
        }
    }
}

