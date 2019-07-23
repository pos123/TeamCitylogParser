using System.Collections.Generic;
using System.Linq;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class Parser
    {
        private readonly IDataService dataService;
        private readonly IValueExtractor valueExtractor;
        
         public List<INoiseEntry> Noise =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<INoiseEntry>(EntryFactory.CreateNoiseEntryFunc, EntryType.Noise())
                .EvaluateToList<INoiseEntry>(valueExtractor, dataService);

        public ISolutionStartEntry SolutionStart =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<ISolutionStartEntry>(EntryFactory.CreateSolutionStartEntryFunc,
                    EntryType.SolutionStart())
                .EvaluateToList<ISolutionStartEntry>(valueExtractor, dataService).FirstOrDefault();

        public ISolutionEndBuildFailedEntry SolutionFailedEntry =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<ISolutionEndBuildFailedEntry>(
                    EntryFactory.CreateSolutionEndBuildFailedEntryFunc, EntryType.SolutionEndBuildFailed())
                .EvaluateToList<ISolutionEndBuildFailedEntry>(valueExtractor, dataService).FirstOrDefault();
        
        public ISolutionEndBuildSucceededEntry SolutionBuildSucceeded =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<ISolutionEndBuildSucceededEntry>(
                    EntryFactory.CreateSolutionEndBuildSucceededEntryFunc, EntryType.SolutionEndBuildSucceeded())
                .EvaluateToList<ISolutionEndBuildSucceededEntry>(valueExtractor, dataService).FirstOrDefault();
        
        public ISolutionEndRebuildSucceededEntry SolutionRebuildSucceeded  =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<ISolutionEndRebuildSucceededEntry>(
                    EntryFactory.CreateSolutionEndRebuildSucceededEntryFunc, EntryType.SolutionEndRebuildSucceeded())
                .EvaluateToList<ISolutionEndRebuildSucceededEntry>(valueExtractor, dataService).FirstOrDefault();
        
        public List<IProjectDefinitionEntry> ProjectDefinitions => 
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectDefinitionEntry>(EntryFactory.CreateProjectDefinitionEntryFunc, EntryType.ProjectDefinition())
                .EvaluateToList<IProjectDefinitionEntry>(valueExtractor, dataService);
        
        public List<IProjectEmptyEntry> ProjectEmptyEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectEmptyEntry>(EntryFactory.CreateProjectEmptyEntryFunc, EntryType.ProjectEmptyEntry())
                .EvaluateToList<IProjectEmptyEntry>(valueExtractor, dataService);
        
        public List<IProjectEndBuildFailedEntry> ProjectBuildFailedEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectEndBuildFailedEntry>(EntryFactory.CreateProjectEndBuildFailedEntryFunc, EntryType.ProjectBuildFailedEntry())
                .EvaluateToList<IProjectEndBuildFailedEntry>(valueExtractor, dataService);
        
        public List<IProjectEndBuildSucceededEntry> ProjectBuildSucceededEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectEndBuildSucceededEntry>(EntryFactory.CreateProjectEndBuildSucceededEntryFunc, EntryType.ProjectBuildSucceededEntry())
                .EvaluateToList<IProjectEndBuildSucceededEntry>(valueExtractor, dataService);
        
        public List<IProjectEntry> ProjectEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectEntry>(EntryFactory.CreateProjectEntryFunc, EntryType.ProjectEntry())
                .EvaluateToList<IProjectEntry>(valueExtractor, dataService);
       
        public List<IProjectEndEntry> ProjectEnd =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectEndEntry>(EntryFactory.CreateProjectEndEntryFunc, EntryType.ProjectEndEntry())
                .EvaluateToList<IProjectEndEntry>(valueExtractor, dataService);
        
        public Parser(IDataService dataService, IValueExtractor valueExtractor)
        {
            this.dataService = dataService;
            this.valueExtractor = valueExtractor;
        }

    }
}
