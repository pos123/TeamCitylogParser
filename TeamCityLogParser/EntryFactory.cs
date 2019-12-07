using System;
using TeamCityLogParser.components;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public static class EntryFactory
    {
        public static Func<Entry> CreateNewEntryFunc(uint lineNumber, EntryType entryType)
        {
            return () => new Entry(entryType, lineNumber);
        }
        
        public static Func<IValueExtractor, IDataService, INoiseEntry> CreateNoiseEntryFunc(uint lineNumber)
        {
            return (extractor, data) => new NoiseEntry(new Entry(EntryType.Noise(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, IProjectDefinitionEntry> CreateProjectDefinitionEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectDefinitionEntry(new Entry(EntryType.ProjectDefinition(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, ISolutionStartEntry> CreateSolutionStartEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new SolutionStartEntry(new Entry(EntryType.SolutionStart(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, ISolutionEndBuildSucceededEntry> CreateSolutionEndBuildSucceededEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new SolutionEndBuildSucceededEntry(new Entry(EntryType.SolutionEndBuildSucceeded(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, ISolutionEndRebuildSucceededEntry> CreateSolutionEndRebuildSucceededEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new SolutionEndRebuildSucceededEntry(new Entry(EntryType.SolutionEndRebuildSucceeded(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, ISolutionEndBuildFailedEntry> CreateSolutionEndBuildFailedEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new SolutionEndBuildBuildFailedEntry(new Entry(EntryType.SolutionEndBuildFailed(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, IProjectEntry> CreateProjectEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectEntry(new Entry(EntryType.ProjectEntry(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, IProjectEmptyEntry> CreateProjectEmptyEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectEmptyEntry(new Entry(EntryType.ProjectEmptyEntry(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, IProjectEndEntry> CreateProjectEndEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectEndEntry(new Entry(EntryType.ProjectEndEntry(), lineNumber), extractor, data);
        }
        
        public static Func<IValueExtractor, IDataService, IProjectEndBuildFailedEntry> CreateProjectEndBuildFailedEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectEndBuildFailedEntry(new Entry(EntryType.ProjectBuildFailedEntry(), lineNumber), extractor, data);
        }    
        
        public static Func<IValueExtractor, IDataService, IProjectEndBuildSucceededEntry> CreateProjectEndBuildSucceededEntryFunc(uint lineNumber) 
        {
            return (extractor, data) => new ProjectEndBuildSucceededEntry(new Entry(EntryType.ProjectBuildSucceededEntry(), lineNumber), extractor, data);
        }
    }
}