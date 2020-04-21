using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.Parsers
{
    public class ProjectLineError
    {
        public IProjectEntry ProjectEntry { get; set; }
        public string Error { get; set; }
    }

    public class CodeParser : IStageParser, ICodeParserResult
    {
        private readonly IDataService dataService;
        private readonly IValueExtractor valueExtractor;
        public List<INoiseEntry> Noise;
        public ISolutionStartEntry SolutionStart;
        public ISolutionEndBuildSucceededEntry SolutionBuildSucceeded;
        public ISolutionEndRebuildSucceededEntry SolutionRebuildSucceeded;
        public List<IProjectDefinitionEntry> ProjectDefinitions;
        public List<IProjectEmptyEntry> ProjectEmptyEntries;
        public List<IProjectEndBuildFailedEntry> ProjectBuildFailedEntries;
        public List<IProjectEndBuildSucceededEntry> ProjectBuildSucceededEntries;
        public List<IProjectEntry> ProjectEntries;
        public List<IProjectEndEntry> ProjectEndEntries;
        public List<ProjectLineError> ProjectLineErrors;

        public CodeParser(IDataService dataService, IValueExtractor valueExtractor)
        {
            this.dataService = dataService;
            this.valueExtractor = valueExtractor;
        }

        public Task Parse(uint start, uint end, Action<string> notification)
        {
            return Task.Run(async () =>
            {
                ProjectLineErrors = new List<ProjectLineError>();

                var taskList = new[]
                {
                    new Task(() => { Noise = GetNoise(start, end); }),
                    new Task(() => { SolutionStart = GetSolutionStart(start, end); }),
                    new Task(() => { SolutionBuildSucceeded = GetSolutionBuildSucceeded(start, end); }),
                    new Task(() => { SolutionRebuildSucceeded = GetSolutionRebuildSucceeded(start, end); }),
                    new Task(() => { ProjectDefinitions = GetProjectDefinitions(start, end); }),
                    new Task(() => { ProjectEmptyEntries = GetProjectEmptyEntries(start, end); }),
                    new Task(() => { ProjectBuildFailedEntries = GetProjectBuildFailedEntries(start, end); }),
                    new Task(() => { ProjectBuildSucceededEntries = GetProjectBuildSucceededEntries(start, end); }),
                    new Task(() => { ProjectEntries = GetProjectEntries(start, end); }),
                    new Task(() => { ProjectEndEntries = GetProjectEndEntries(start, end); }),
                };

                // parse the log in to categories
                notification("parsing build log ...");
                await Task.Delay(500);

                foreach (var task in taskList)
                {
                    task.Start();
                }

                Task.WaitAll(taskList);

                notification("finished parsing build log ...");
                await Task.Delay(500);

                // identify errors into groups
                notification("starting identification of errors ...");
                await Task.Delay(500);

                var i = 0;
                var totalProjectEntries = ProjectEntries.Count;
                var setProjectFailedProjectIDs = new HashSet<uint>(ProjectBuildFailedEntries.Select(x => x.Id));
                foreach (var entry in ProjectEntries)
                {
                    if (setProjectFailedProjectIDs.Contains(entry.ProjectId))
                    {
                        var entryErrorType = entry.ErrorType;
                        if (!string.IsNullOrEmpty(entryErrorType))
                        {
                            ProjectLineErrors.Add(new ProjectLineError()
                            {
                                ProjectEntry = entry,
                                Error = entry.ErrorType
                            });
                        }
                    }

                    if (++i % 50 == 0)
                    {
                        notification($"starting identification of errors ... {++i} / {totalProjectEntries} entries processed");
                    }
                }


                notification("finished error identification ...");
                await Task.Delay(500);
            });
        }

        public Tuple<bool, string> GetStatement()
        {
            var solutionBuild = SolutionBuildSucceeded;
            if (solutionBuild != null)
            {
                return Tuple.Create(true, $"Build: {solutionBuild.Succeeded} succeeded, {solutionBuild.Failed} failed, {solutionBuild.Succeeded} up-to-date, {solutionBuild.Failed} skipped");
            }

            var solutionRebuild = SolutionRebuildSucceeded;
            if (solutionRebuild != null)
            {
                return Tuple.Create(true, $"Rebuild All: {solutionRebuild.Succeeded} succeeded, {solutionRebuild.Failed} failed, {solutionRebuild.Failed} skipped");
            }

            var failedMessage = $"Solution build failed: {ProjectBuildFailedEntries.Count} failed project(s), {ProjectLineErrors.Count} error instance(s)";
            return Tuple.Create(false, failedMessage);
        }

        public IEnumerable<uint> GetFailedProjectList() => ProjectBuildFailedEntries.Select(x => x.Id);

        public List<Tuple<uint, string, string, string, string>> GetBuildErrorsOutputForProject(uint projectId)
        {
            return (from entry in ProjectLineErrors.Where(x => x.ProjectEntry.ProjectId == projectId)
                let lineNumber = entry.ProjectEntry.LineNumber
                let project = ProjectDefinitions.FirstOrDefault(x => x.Id == entry.ProjectEntry.ProjectId)?.Name
                let configuration = ProjectDefinitions.FirstOrDefault(x => x.Id == entry.ProjectEntry.ProjectId)?.Configuration
                let errorCategory = entry.Error
                let error = entry.ProjectEntry.Data
                select Tuple.Create(lineNumber, project, configuration, errorCategory, error)).ToList();
        }


        public List<Tuple<uint, string, string, string, string>> GetBuildErrorsOutput()
        {
            return (from entry in ProjectLineErrors.OrderBy(x => x.ProjectEntry.ProjectId).ThenBy(x => x.ProjectEntry.LineNumber)
                    let lineNumber = entry.ProjectEntry.LineNumber
                    let project = ProjectDefinitions.FirstOrDefault(x => x.Id == entry.ProjectEntry.ProjectId)?.Name
                    let configuration = ProjectDefinitions.FirstOrDefault(x => x.Id == entry.ProjectEntry.ProjectId)?.Configuration
                    let errorCategory = entry.Error
                    let error = entry.ProjectEntry.Data
                    select Tuple.Create(lineNumber, project, configuration, errorCategory, error)).ToList();
        }

        public IEnumerable<Tuple<uint, string>> GetProjectData(uint projectId)
        {
            return ProjectEntries.Where(x => x.ProjectId == projectId)
                .OrderBy(x => x.LineNumber)
                .Select(x => Tuple.Create(x.LineNumber, x.Data));
        }

        public string GetSortedProjectData()
        {
            var builder = new StringBuilder(6000000);
            foreach (var projectDefinition in ProjectDefinitions.OrderBy(x => x.Id))
            {
                builder.AppendLine("========================================================================");
                builder.AppendLine($" {projectDefinition.Name}, {projectDefinition.Configuration}");
                builder.AppendLine("========================================================================");
                foreach (var lineData in GetProjectData(projectDefinition.Id))
                {
                    builder.AppendLine(lineData.Item2);
                }
                builder.AppendLine(Environment.NewLine);
                builder.AppendLine(Environment.NewLine);
            }
            return builder.ToString();
        }

        public int GetErrorCount()
        {
            return GetBuildErrorsOutput().Count;
        }

        public List<ProjectLineError> GetProjectLineErrors() => ProjectLineErrors;
        public List<IProjectDefinitionEntry> GetProjectDefinitions() => ProjectDefinitions;
        public List<IProjectEntry> GetProjectEntries() => ProjectEntries;


        private List<INoiseEntry> GetNoise(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<INoiseEntry>(EntryFactory.CreateNoiseEntryFunc, EntryType.Noise())
                .EvaluateToList<INoiseEntry>(valueExtractor, dataService);

        private ISolutionStartEntry GetSolutionStart(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<ISolutionStartEntry>(EntryFactory.CreateSolutionStartEntryFunc,
                    EntryType.SolutionStart())
                .EvaluateToList<ISolutionStartEntry>(valueExtractor, dataService).FirstOrDefault();

        private ISolutionEndBuildSucceededEntry GetSolutionBuildSucceeded(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<ISolutionEndBuildSucceededEntry>(
                    EntryFactory.CreateSolutionEndBuildSucceededEntryFunc, EntryType.SolutionEndBuildSucceeded())
                .EvaluateToList<ISolutionEndBuildSucceededEntry>(valueExtractor, dataService).FirstOrDefault();

        private ISolutionEndRebuildSucceededEntry GetSolutionRebuildSucceeded(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<ISolutionEndRebuildSucceededEntry>(
                    EntryFactory.CreateSolutionEndRebuildSucceededEntryFunc, EntryType.SolutionEndRebuildSucceeded())
                .EvaluateToList<ISolutionEndRebuildSucceededEntry>(valueExtractor, dataService).FirstOrDefault();

        private List<IProjectDefinitionEntry> GetProjectDefinitions(uint start, uint end) => 
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectDefinitionEntry>(EntryFactory.CreateProjectDefinitionEntryFunc, EntryType.ProjectDefinition())
                .EvaluateToList<IProjectDefinitionEntry>(valueExtractor, dataService);

        private List<IProjectEmptyEntry> GetProjectEmptyEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectEmptyEntry>(EntryFactory.CreateProjectEmptyEntryFunc, EntryType.ProjectEmptyEntry())
                .EvaluateToList<IProjectEmptyEntry>(valueExtractor, dataService);

        private List<IProjectEndBuildFailedEntry> GetProjectBuildFailedEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectEndBuildFailedEntry>(EntryFactory.CreateProjectEndBuildFailedEntryFunc, EntryType.ProjectBuildFailedEntry())
                .EvaluateToList<IProjectEndBuildFailedEntry>(valueExtractor, dataService);

        private List<IProjectEndBuildSucceededEntry> GetProjectBuildSucceededEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectEndBuildSucceededEntry>(EntryFactory.CreateProjectEndBuildSucceededEntryFunc, EntryType.ProjectBuildSucceededEntry())
                .EvaluateToList<IProjectEndBuildSucceededEntry>(valueExtractor, dataService);

        private List<IProjectEntry> GetProjectEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectEntry>(EntryFactory.CreateProjectEntryFunc, EntryType.ProjectEntry())
                .EvaluateToList<IProjectEntry>(valueExtractor, dataService);

        private List<IProjectEndEntry> GetProjectEndEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.CodeBuildParserEntryTypes)
                .FilterEntryDefinitionTypeFunc<IProjectEndEntry>(EntryFactory.CreateProjectEndEntryFunc, EntryType.ProjectEndEntry())
                .EvaluateToList<IProjectEndEntry>(valueExtractor, dataService);
    }
}
