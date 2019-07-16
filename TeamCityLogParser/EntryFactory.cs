using TeamCityLogParser.components;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public static class EntryFactory
    {
        public static IProjectDefinitionEntry CreateProjectDefinitionEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectDefinitionEntry(new Entry(EntryType.ProjectDefinition(), lineNumber), valueExtractor, dataService);
        }
        
        public static ISolutionStartEntry CreateSolutionStartEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new SolutionStartEntry(new Entry(EntryType.SolutionStart(), lineNumber), valueExtractor, dataService);
        }
        
        public static ISolutionEndBuildSucceededEntry CreateSolutionEndBuildSucceededEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new SolutionEndBuildSucceededEntry(new Entry(EntryType.SolutionEndBuildSucceeded(), lineNumber), valueExtractor, dataService);
        }
        
        public static ISolutionEndRebuildSucceededEntry CreateSolutionEndRebuildSucceededEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new SolutionEndRebuildSucceededEntry(new Entry(EntryType.SolutionEndRebuildSucceeded(), lineNumber), valueExtractor, dataService);
        }
        
        public static ISolutionEndBuildFailed CreateSolutionEndBuildFailedEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new SolutionEndBuildBuildFailed(new Entry(EntryType.SolutionEndBuildFailed(), lineNumber), valueExtractor, dataService);
        }
        
        public static IProjectEntry CreateProjectEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectEntry(new Entry(EntryType.ProjectEntry(), lineNumber), valueExtractor, dataService);
        }
        
        public static IProjectEmptyEntry CreateProjectEmptyEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectEmptyEntry(new Entry(EntryType.ProjectEmptyEntry(), lineNumber), valueExtractor, dataService);
        }
        
        public static IProjectEndEntry CreateProjectEndEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectEndEntry(new Entry(EntryType.ProjectEndEntry(), lineNumber), valueExtractor, dataService);
        }
        
        public static IProjectEndBuildFailedEntry CreateProjectEndBuildFailedEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectEndBuildFailedEntry(new Entry(EntryType.ProjectBuildFailedEntry(), lineNumber), valueExtractor, dataService);
        }
        
        public static IProjectEndBuildSucceededEntry CreateProjectEndBuildSucceededEntry(uint lineNumber, IValueExtractor valueExtractor, IDataService dataService) 
        {
            return new ProjectEndBuildSucceededEntry(new Entry(EntryType.ProjectBuildSucceededEntry(), lineNumber), valueExtractor, dataService);
        }
    }
}