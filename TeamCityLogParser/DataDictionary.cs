using System.Collections.Generic;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class DataDictionary : IDataDictionary
    {

        private readonly Dictionary<uint, string> definitionMap = new Dictionary<uint, string>();

        private const string LineStart = @"^";
        private const string LineEnd = @"$";
        private const string Comma = @",";
        private const string WhiteSpaceOneOrMore = @"\s+";
        private const string WhiteSpaceOne = @"\s";
        private const string Time = @"\[(?<time>\d{2}:\d{2}:\d{2})\]";
        private const string Colon = @":";
        private const string Exec = @"(?<exec>\[exec])";
        private const string ProjectId = @"(?<projectId>\d+)";
        private const string Arrow = @">";
        private const string ProjectBuildStarted = @"(Build started|Rebuild All started): ";
        private const string Project = @"Project: ";
        private const string ProjectConfiguration = @"(?<projectConfiguration>.+(?=(\s-)))";
        private const string ProjectName = @"(?<projectName>.*)";
        private const string AnyZeroOrMore = @".*";
        private const string Configuration = @"Configuration";
        private const string DashesOneOrMore = @"-+";
        private const string DashesOne = @"-";
        private const string BuildAccelerationConsole = @"(?<buildAccelerationConsole>Build Acceleration Console) .+";
        private const string Equals10 = @"={10}";
        private const string Build = @"Build";
        private const string ReBuild = @"Rebuild All";
        private const string BuildSucceeded = @"(?<buildSucceeded>\d+)\ssucceeded";
        private const string BuildFailed = @"(?<buildFailed>\d+)\sfailed";
        private const string BuildUpToDate = @"(?<upToDate>\d+)\sup-to-date";
        private const string BuildSkipped = @"(?<buildSkipped>\d+)\sskipped";
        private const string WOne = @"W";
        private const string NantBuildFailed = @"\[NAnt output\] BUILD FAILED";
        private const string NonFatalErrors = @"(?<buildFailedNonFatalErrors>\d+)\snon-fatal error\(s\)";
        private const string BuildFailedWarnings = @"(?<buildFailedWarnings>\d+)\swarning\(s\)";
        private const string ProjectLineData = @"(?<projectLineData>.+)";
        private const string TimeElapsed = @"Time Elapsed (?<timeElapsed>\d\d:\d\d:\d\d.\d\d)";
        private const string ProjectBuildFailed = @"(?<buildFailed>Build FAILED.)";
        private const string ProjectBuildSucceeded = @"(?<buildSucceeded>Build succeeded.)";
        
        public DataDictionary()
        {    
            // Project definition
            // [10:54:44] :          [exec] 158>------ Build started: Project: blah x x x, Configuration: Release Win32 ------
            definitionMap[EntryType.ProjectDefinition().Id] = LineStart + 
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
                                                              Project +
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
                                                              LineEnd;
            
            // Solution start
            // [10:54:44] :          [exec] Build Acceleration Console 8.0.1 (build 1867)
            definitionMap[EntryType.SolutionStart().Id] = LineStart + 
                                                           Time + 
                                                           WhiteSpaceOne + 
                                                           Colon + 
                                                           WhiteSpaceOneOrMore + 
                                                           Exec + 
                                                           WhiteSpaceOne +
                                                           BuildAccelerationConsole +
                                                           LineEnd;
            // Solution end build succeeded
            // [10:54:44] :          [exec] ========== Build: 35 succeeded, 0 failed, 5 up-to-date, 326 skipped ==========  
            definitionMap[EntryType.SolutionEndBuildSucceeded().Id] = LineStart + 
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
                                                                       LineEnd;
            
            
            // Solution end rebuild all succeeded 
            // [10:54:44] :          [exec] ========== Rebuild All: 35 succeeded, 0 failed, 326 skipped ==========
            definitionMap[EntryType.SolutionEndRebuildSucceeded().Id] = LineStart + 
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
                                                                        LineEnd;   
            
            // Solution end failed
            // [10:54:44]W:          [NAnt output] BUILD FAILED - 8 non-fatal error(s), 15 warning(s)    
            definitionMap[EntryType.SolutionEndBuildFailed().Id] = LineStart + 
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
                                                                   LineEnd;
            
            // Project entry type
            // [10:53:29] :            [exec] 44> blah blah blah
            definitionMap[EntryType.ProjectEntry().Id] =  LineStart +
                                                          Time +
                                                          WhiteSpaceOne +
                                                          Colon +
                                                          WhiteSpaceOneOrMore +
                                                          Exec +
                                                          WhiteSpaceOne +
                                                          ProjectId +
                                                          Arrow +
                                                          ProjectLineData +
                                                          LineEnd;
            
            // Project entry empty type
            // [10:53:29] :            [exec] 44>
            definitionMap[EntryType.ProjectEmptyEntry().Id] = LineStart +
                                                              Time +
                                                              WhiteSpaceOne +
                                                              Colon +
                                                              WhiteSpaceOneOrMore +
                                                              Exec +
                                                              WhiteSpaceOne +
                                                              ProjectId +
                                                              Arrow +
                                                              LineEnd;
            
            // Project end entry
            // [19:07:17] :          [exec] 54>Time Elapsed 00:00:14.56
            definitionMap[EntryType.ProjectEndEntry().Id] = LineStart +
                                                               Time +
                                                               WhiteSpaceOne +
                                                               Colon +
                                                               WhiteSpaceOneOrMore +
                                                               Exec +
                                                               WhiteSpaceOne +
                                                               ProjectId +
                                                               Arrow +
                                                               TimeElapsed +              
                                                               LineEnd; 
            
            // Project build failed
            // [19:07:17] :       [exec] 27>Build FAILED.
            definitionMap[EntryType.ProjectBuildFailedEntry().Id] = LineStart +
                                                                     Time +
                                                                     WhiteSpaceOne +
                                                                     Colon +
                                                                     WhiteSpaceOneOrMore +
                                                                     Exec +
                                                                     WhiteSpaceOne +
                                                                     ProjectId +
                                                                     Arrow +
                                                                     ProjectBuildFailed +              
                                                                     LineEnd;
            
            // Project build succeeded
            // [19:07:17] :       [exec] 27>Build succeeded.
            definitionMap[EntryType.ProjectBuildSucceededEntry().Id] = LineStart +
                                                                     Time +
                                                                     WhiteSpaceOne +
                                                                     Colon +
                                                                     WhiteSpaceOneOrMore +
                                                                     Exec +
                                                                     WhiteSpaceOne +
                                                                     ProjectId +
                                                                     Arrow +
                                                                     ProjectBuildSucceeded +              
                                                                     LineEnd;
            
            
        }

        public Dictionary<uint, string> GetDictionary()
        {
            return definitionMap;
        }
    }
}