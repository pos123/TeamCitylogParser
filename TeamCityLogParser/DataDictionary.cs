using System.Collections.Generic;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class DataDictionary : IDataDictionary
    {

        private readonly Dictionary<uint, string> definitionMap = new Dictionary<uint, string>();
       
        private static string NamedAny(string name) => $@"(?<{name}>.*)";

        private static readonly string LineStart = @"^";
        private static readonly string LineEnd = @"$";
        private static readonly string Comma = @",";
        private static readonly string WhiteSpaceZeroOrMore = @"\s*";
        private static readonly string WhiteSpaceOneOrMore = @"\s+";
        private static readonly string WhiteSpaceOne = @"\s";
        
        private static readonly string Time = @"(?<time>\[\d{2}:\d{2}:\d{2}\])";
        private static readonly string Colon = @":";
        private static readonly string Exec = @"(?<exec>\[exec])";
        private static readonly string ProjectId = @"(?<projectId>\d+)";
        private static readonly string Arrow = @">";
        private static readonly string ProjectBuildStarted = @"Build started: Project: ";
        private static readonly string ProjectConfiguration = @"(?<projectConfiguration>.+(?=(\s-)))";
        private static readonly string ProjectName = NamedAny("projectName");
        private static readonly string AnyZeroOrMore = @".*";
        private static readonly string Configuration = @"Configuration";
        private static readonly string DashesOneOrMore = @"-+";
        private static readonly string DashesOne = @"-";
        private static readonly string BuildAccelerationConsole = @"(?<buildAccelerationConsole>Build Acceleration Console) .+";
        private static readonly string Equals10 = @"={10}";
        private static readonly string Build = @"Build";
        private static readonly string ReBuild = @"Rebuild All";
        private static readonly string BuildSucceeded = @"(?<buildSucceeded>\d+)\ssucceeded";
        private static readonly string BuildFailed = @"(?<buildFailed>\d+)\sfailed";
        private static readonly string BuildUpToDate = @"(?<upToDate>\d+)\sup-to-date";
        private static readonly string BuildSkipped = @"(?<buildSkipped>\d+)\sskipped";
        private static readonly string WOne = @"W";
        private static readonly string NantBuildFailed = @"\[NAnt output\] BUILD FAILED";
        private static readonly string NonFatalErrors = @"(?<buildFailedNonFatalErrors>\d+)\snon-fatal error\(s\)";
        private static readonly string BuildFailedWarnings = @"(?<buildFailedWarnings>\d+)\swarning\(s\)";
        private static readonly string ProjectLineData = @"(?<projectLineData>.+)";
        
        public DataDictionary()
        {    
            
            // Project definition
            // [10:54:44] :          [exec] 158>------ Build started: Project: blah x x x, Configuration: Release Win32 ------
            definitionMap[EntryType.ProjectDefinition().id] = (LineStart + 
                                                               WhiteSpaceOne + 
                                                               Time + 
                                                               WhiteSpaceOne + 
                                                               Colon + 
                                                               WhiteSpaceOneOrMore + 
                                                               Exec + 
                                                               WhiteSpaceOne +
                                                               ProjectId +
                                                               Arrow +
                                                               DashesOneOrMore +
                                                               WhiteSpaceOne +
                                                               ProjectBuildStarted +
                                                               ProjectName +
                                                               AnyZeroOrMore +
                                                               Comma + 
                                                               WhiteSpaceOne +
                                                               Configuration +
                                                               Colon +
                                                               WhiteSpaceOne +
                                                               ProjectConfiguration +
                                                               WhiteSpaceOne +
                                                               DashesOneOrMore +
                                                               WhiteSpaceZeroOrMore +
                                                               LineEnd);
            
            // Solution start
            // [10:54:44] :          [exec] Build Acceleration Console 8.0.1 (build 1867)
            definitionMap[EntryType.SolutionStart().id] = (LineStart + 
                                                           WhiteSpaceOne + 
                                                           Time + 
                                                           WhiteSpaceOne + 
                                                           Colon + 
                                                           WhiteSpaceOneOrMore + 
                                                           Exec + 
                                                           WhiteSpaceOne +
                                                           BuildAccelerationConsole +
                                                           WhiteSpaceZeroOrMore +
                                                           LineEnd);
            // Solution end build succeeded
            // [10:54:44] :          [exec] ========== Build: 35 succeeded, 0 failed, 5 up-to-date, 326 skipped ==========  
            definitionMap[EntryType.SolutionEndBuildSucceeded().id] = (LineStart + 
                                                                       WhiteSpaceOne + 
                                                                       Time + 
                                                                       WhiteSpaceOne + 
                                                                       Colon + 
                                                                       WhiteSpaceOneOrMore + 
                                                                       Exec +
                                                                       WhiteSpaceOne +
                                                                       Equals10 +  
                                                                       WhiteSpaceOne +
                                                                       Build + 
                                                                       Colon +
                                                                       WhiteSpaceOne +
                                                                       BuildSucceeded + 
                                                                       Comma +
                                                                       WhiteSpaceOne +
                                                                       BuildFailed + 
                                                                       Comma +
                                                                       WhiteSpaceOne +
                                                                       BuildUpToDate + 
                                                                       Comma +
                                                                       WhiteSpaceOne +
                                                                       BuildSkipped +
                                                                       WhiteSpaceOne +
                                                                       Equals10 +  
                                                                       WhiteSpaceZeroOrMore +
                                                                       LineEnd);
            
            
            // Solution end rebuild all succeeded 
            // [10:54:44] :          [exec] ========== Rebuild All: 35 succeeded, 0 failed, 326 skipped ==========
            definitionMap[EntryType.SolutionEndRebuildSucceeded().id] = (LineStart + 
                                                                        WhiteSpaceOne + 
                                                                        Time + 
                                                                        WhiteSpaceOne + 
                                                                        Colon + 
                                                                        WhiteSpaceOneOrMore + 
                                                                        Exec +
                                                                        WhiteSpaceOne +
                                                                        Equals10 +  
                                                                        WhiteSpaceOne +
                                                                        ReBuild + 
                                                                        Colon +
                                                                        WhiteSpaceOne +
                                                                        BuildSucceeded + 
                                                                        Comma +
                                                                        WhiteSpaceOne +
                                                                        BuildFailed + 
                                                                        Comma +
                                                                        WhiteSpaceOne +
                                                                        BuildSkipped +
                                                                        WhiteSpaceOne +
                                                                        Equals10 +  
                                                                        WhiteSpaceZeroOrMore +
                                                                        LineEnd);   
            
            // Solution end failed
            // [10:54:44]W:          [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)    
            definitionMap[EntryType.SolutionEndBuildFailed().id] = (LineStart + 
                                                                   WhiteSpaceOne +
                                                                   Time +
                                                                   WOne +
                                                                   Colon +
                                                                   WhiteSpaceOneOrMore +
                                                                   NantBuildFailed +
                                                                   WhiteSpaceOne +
                                                                   DashesOne +
                                                                   WhiteSpaceOne +
                                                                   NonFatalErrors + 
                                                                   Comma +
                                                                   WhiteSpaceOne +
                                                                   BuildFailedWarnings +     
                                                                   WhiteSpaceZeroOrMore +
                                                                   LineEnd);
            
            // Project entry type
            // [10:53:29] :            [exec] 44> blah blah blah
            definitionMap[EntryType.ProjectEntry().id] = (LineStart +
                                                          WhiteSpaceOne +
                                                          Time +
                                                          WhiteSpaceOne +
                                                          Colon +
                                                          WhiteSpaceOneOrMore +
                                                          Exec +
                                                          WhiteSpaceOne +
                                                          ProjectId +
                                                          Arrow +
                                                          WhiteSpaceOne +
                                                          ProjectLineData +
                                                          WhiteSpaceZeroOrMore +
                                                          LineEnd);
            
            // Project entry empty type
            // [10:53:29] :            [exec] 44>
            definitionMap[EntryType.ProjectEmpty().id] = (LineStart +
                                                          WhiteSpaceOne +
                                                          Time +
                                                          WhiteSpaceOne +
                                                          Colon +
                                                          WhiteSpaceOneOrMore +
                                                          Exec +
                                                          WhiteSpaceOne +
                                                          ProjectId +
                                                          Arrow +
                                                          LineEnd);
            
        }

        public Dictionary<uint, string> GetDictionary()
        {
            return definitionMap;
        }
    }
}