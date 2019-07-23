using System.Collections.Generic;
using System.Threading.Tasks;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class ParserParallelExecution
    {
        private readonly Parser parser;
        private readonly Task[] taskList;
        public List<INoiseEntry> Noise;
        public ISolutionStartEntry SolutionStart;
        public ISolutionEndBuildFailedEntry SolutionFailedEntry;
        public ISolutionEndBuildSucceededEntry SolutionBuildSucceeded;
        public ISolutionEndRebuildSucceededEntry SolutionRebuildSucceeded;
        public List<IProjectDefinitionEntry> ProjectDefinitions;
        public List<IProjectEmptyEntry> ProjectEmptyEntries;
        public List<IProjectEndBuildFailedEntry> ProjectBuildFailedEntries;
        public List<IProjectEndBuildSucceededEntry> ProjectBuildSucceededEntries;
        public List<IProjectEntry> ProjectEntries;
        public List<IProjectEndEntry> ProjectEnd;

        public ParserParallelExecution(Parser parser)
        {
            this.parser = parser;
            taskList = new Task[]
            {
                new Task(() => { Noise = parser.Noise; }),
                new Task(() => { SolutionStart = parser.SolutionStart; }),
                new Task(() => { SolutionFailedEntry = parser.SolutionFailedEntry; }),
                new Task(() => { SolutionBuildSucceeded = parser.SolutionBuildSucceeded; }),
                new Task(() => { SolutionRebuildSucceeded = parser.SolutionRebuildSucceeded; }),
                new Task(() => { ProjectDefinitions = parser.ProjectDefinitions; }),
                new Task(() => { ProjectEmptyEntries = parser.ProjectEmptyEntries; }),
                new Task(() => { ProjectBuildFailedEntries = parser.ProjectBuildFailedEntries; }),
                new Task(() => { ProjectBuildSucceededEntries = parser.ProjectBuildSucceededEntries; }),
                new Task(() => { ProjectEntries = parser.ProjectEntries; }),
                new Task(() => { ProjectEnd = parser.ProjectEnd; }),
            };
        }

        public Task Run()
        {
            return Task.Run(() =>
            {
                foreach (var task in taskList)
                {
                    task.Start();
                }

                Task.WaitAll(taskList);
            });
        }
    }
}