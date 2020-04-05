using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class ParserParallelExecution
    {
        private readonly Action<uint> finishedStatus;
        private readonly int delay;
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

        public ParserParallelExecution(Parser parser, Action<uint> finishedStatus = null, int delay = 0)
        {
            this.finishedStatus = finishedStatus;
            this.delay = delay;
            taskList = new Task[]
            {
                new Task(() => { Noise = parser.Noise; finishedStatus?.Invoke(EntryType.Noise().Id); }),
                new Task(() => { SolutionStart = parser.SolutionStart; finishedStatus?.Invoke(EntryType.SolutionStart().Id); }),
                new Task(() => { SolutionFailedEntry = parser.SolutionFailedEntry; finishedStatus?.Invoke(EntryType.SolutionEndBuildFailed().Id); }),
                new Task(() => { SolutionBuildSucceeded = parser.SolutionBuildSucceeded; finishedStatus?.Invoke(EntryType.SolutionEndBuildSucceeded().Id); }),
                new Task(() => { SolutionRebuildSucceeded = parser.SolutionRebuildSucceeded; finishedStatus?.Invoke(EntryType.SolutionEndRebuildSucceeded().Id); }),
                new Task(() => { ProjectDefinitions = parser.ProjectDefinitions; finishedStatus?.Invoke(EntryType.ProjectDefinition().Id); }),
                new Task(() => { ProjectEmptyEntries = parser.ProjectEmptyEntries; finishedStatus?.Invoke(EntryType.ProjectEmptyEntry().Id); }),
                new Task(() => { ProjectBuildFailedEntries = parser.ProjectBuildFailedEntries; finishedStatus?.Invoke(EntryType.ProjectBuildFailedEntry().Id); }),
                new Task(() => { ProjectBuildSucceededEntries = parser.ProjectBuildSucceededEntries; finishedStatus?.Invoke(EntryType.ProjectBuildSucceededEntry().Id); }),
                new Task(() => { ProjectEntries = parser.ProjectEntries; finishedStatus?.Invoke(EntryType.ProjectEntry().Id); }),
                new Task(() => { ProjectEnd = parser.ProjectEnd; finishedStatus?.Invoke(EntryType.ProjectEndEntry().Id); }),
            };
        }

        public Task Run()
        {
            return Task.Run(async () =>
            {
                foreach (var task in taskList)
                {
                    task.Start();
                    await Task.Delay(delay);
                }

                Task.WaitAll(taskList);
            });
        }
    }
}