using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.Parsers
{
    public class DefaultStageParser : IStageParser, IDefaultStageParserResult
    {
        private readonly StageGroupType stageGroupType;
        private readonly IDataService dataService;
        private readonly IValueExtractor valueExtractor;

        public List<IDefaultErrorEntry> Errors { get; private set; }

        public DefaultStageParser(StageGroupType stageGroupType, IDataService dataService, IValueExtractor valueExtractor)
        {
            this.stageGroupType = stageGroupType;
            this.dataService = dataService;
            this.valueExtractor = valueExtractor;
        }
        
        public Task Parse(uint start, uint end, Action<string> notification)
        {
            notification("start retrieving errors");
            Errors = DefaultErrorEntries(start, end);
            notification("finished retrieving errors");
            return Task.CompletedTask;
        }

        public Tuple<bool, string> GetStatement()
        {
            var description = stageGroupType == StageGroupType.VerifyPackages ? "Verify Packages" : "SVN Update";
            return new Tuple<bool, string>(false, $"{Errors.Count} error entries(s) found in stage - {description}");
        }

        public int GetErrorCount()
        {
            return Errors.Count;
        }

        private List<IDefaultErrorEntry> DefaultErrorEntries(uint start, uint end) =>
            dataService.FilteredData(start, end).MapCalcEntryTypeFunc(valueExtractor, EntryType.DefaultEntryTypes)
                .FilterEntryDefinitionTypeFunc(EntryFactory.CreateDefaultErrorEntryFunc, EntryType.DefaultErrorEntry())
                .EvaluateToList(valueExtractor, dataService);

        public List<Tuple<uint, string>> GetErrors()
        {
            return Errors.Select(error => new Tuple<uint, string>(error.LineNumber, error.Error)).ToList();
        }
    }
}
