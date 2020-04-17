using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.Parsers
{
    public class BuildLogParser
    {
        private readonly IDataService dataService;
        private readonly IValueExtractor valueExtractor;
        private readonly Dictionary<StageGroupType, IStageParser> stageParsers;
        
        public List<StageGroup> StageGroups { get; private set; } = new List<StageGroup>();

        public IDefaultStageParserResult VerifyPackageResults => stageParsers[StageGroupType.VerifyPackages] as IDefaultStageParserResult;
        public IDefaultStageParserResult SvnUpdateResults => stageParsers[StageGroupType.SvnUpdate] as IDefaultStageParserResult;
        public ICodeParserResult CodeResults => stageParsers[StageGroupType.CodeBuild] as ICodeParserResult;


        public BuildLogParser(string payload)
        {
            dataService = new DataService(payload);
            valueExtractor = new ValueExtractor(new DataDictionary());

            stageParsers = new Dictionary<StageGroupType, IStageParser>()
            {
                { StageGroupType.SvnUpdate, new DefaultStageParser(dataService, valueExtractor) },
                { StageGroupType.VerifyPackages, new DefaultStageParser(dataService, valueExtractor) },
                { StageGroupType.CodeBuild, new CodeParser(dataService, valueExtractor) }
            };
        }

        public async Task Parse(Action<string> notification)
        {
            IdentifyStages(notification);

            foreach (var stage in StageGroups.OrderBy(x => x.GroupStageNo))
            {
                if (stage.IsStageFailure || stage.StageGroupType == StageGroupType.CodeBuild)
                {
                    var (start, end) = stage.StageLineRange;
                    await stageParsers[stage.StageGroupType].Parse(start, end, notification);
                }
            }
        }

        public Tuple<bool, string> GetStatement()
        {
            if (!StageGroups.Any())
            {
                return Tuple.Create(false, "no known build stages found in log");
            }

            var failedStage = StageGroups.OrderBy(x => x.GroupStageNo).FirstOrDefault(x => x.IsStageFailure);
            if (failedStage != null)
            {
                return stageParsers[failedStage.StageGroupType].GetStatement();
            }

            var lastStageProcessed = StageGroups.OrderByDescending(x => x.GroupStageNo).FirstOrDefault();
            if (lastStageProcessed != null)
            {
                return stageParsers[lastStageProcessed.StageGroupType].GetStatement();
            }

            return Tuple.Create(false, "unable to provide build log parse statement");
        }

        public TimeSpan? GetStageTimeTaken(StageGroupType stage)
        {
            return StageGroups.FirstOrDefault(x => x.StageGroupType == stage)?.StageTime;
        }

        private void IdentifyStages(Action<string> notification)
        {
            notification("start identifying build stages");

            StageGroups = new List<StageGroup>();
            var stageSkipped = StageSkippedEntries.ToList();
            var stageExits = StageExitEntries.ToList();
            foreach (var stageStart in StageStartEntries)
            {
                if (stageSkipped.FirstOrDefault(x => x.StageNo == stageStart.StageNo) != null)
                {
                    continue;
                }

                var stageExit = stageExits.FirstOrDefault(x => x.StageNo == stageStart.StageNo);
                if (stageExit != null)
                {
                    var stageType = DetermineStageType(stageStart.StageLabel);
                    if (stageType != StageGroupType.Unknown)
                    {
                        StageGroups.Add(new StageGroup()
                        {
                            StageStart = stageStart,
                            StageExit = stageExit,
                            StageGroupType = stageType
                        });
                    }
                    
                }
            }

            notification("finished identifying build stages");
        }

        private static StageGroupType DetermineStageType(string label)
        {
            if (label.ToLower().Contains(Constants.StageSvnUpdate))
            {
                return StageGroupType.SvnUpdate;
            }

            if (label.ToLower().Contains(Constants.StageVerifyPackages))
            {
                return StageGroupType.VerifyPackages;
            }

            return label.ToLower().Contains(Constants.StageNant) ? StageGroupType.CodeBuild : StageGroupType.Unknown;
        }

        private IEnumerable<IStageStartType> StageStartEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor, EntryType.StageEntryTypes)
                .FilterEntryDefinitionTypeFunc<IStageStartType>(EntryFactory.CreateStageStartEntryFunc, EntryType.StageStartType())
                .EvaluateToList<IStageStartType>(valueExtractor, dataService);

        private IEnumerable<IStageExitType> StageExitEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor, EntryType.StageEntryTypes)
                .FilterEntryDefinitionTypeFunc<IStageExitType>(EntryFactory.CreateStageExitEntryFunc, EntryType.StageExitType())
                .EvaluateToList<IStageExitType>(valueExtractor, dataService);

        private IEnumerable<IStageSkippedType> StageSkippedEntries =>
            dataService.Data().MapCalcEntryTypeFunc(valueExtractor, EntryType.StageEntryTypes)
                .FilterEntryDefinitionTypeFunc<IStageSkippedType>(EntryFactory.CreateStageSkippedEntryFunc, EntryType.StageSkippedType())
                .EvaluateToList<IStageSkippedType>(valueExtractor, dataService);
    }
}
