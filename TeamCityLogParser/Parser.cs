using System;
using System.Linq;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class Parser
    {
        private readonly IDataService dataService;
        private readonly IValueExtractor valueExtractor;
        
        public Parser(string data)
        {
            dataService = new DataService(data);
            valueExtractor = new ValueExtractor(new DataDictionary());
        }

        public void Run()
        {
            var noiseEntries = dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                                     .FilterEntryDefinitionTypeFunc<INoiseEntry>(EntryFactory.CreateNoiseEntryFunc, EntryType.Noise())
                                     .EvaluateToList<INoiseEntry>(valueExtractor, dataService);

            var solutionStart = dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                                    .FilterEntryDefinitionTypeFunc<ISolutionStartEntry>(EntryFactory.CreateSolutionStartEntryFunc, EntryType.SolutionStart())
                                    .EvaluateToList<ISolutionStartEntry>(valueExtractor, dataService);
 
            var projectDefinitions = dataService.Data().MapCalcEntryTypeFunc(valueExtractor)
                .FilterEntryDefinitionTypeFunc<IProjectDefinitionEntry>(EntryFactory.CreateProjectDefinitionEntryFunc, EntryType.ProjectDefinition())
                .EvaluateToList<IProjectDefinitionEntry>(valueExtractor, dataService);
            
            
            
            foreach (var result in solutionStart.AsParallel())
            {
                ISolutionStartEntry x = result;
                var entryType = x.EntryType;
                var lineNum = x.LineNumber;
                Console.WriteLine($"{entryType.Id} {lineNum} {x.SolutionStart}");
            }
            
            foreach (var result in noiseEntries.AsParallel().AsOrdered())
            {
                INoiseEntry x = result;
                var entryType = x.EntryType;
                var lineNum = x.LineNumber;
                Console.WriteLine($"{entryType.Id} {lineNum}");
            }
            
            //FilterEntryDefinitionTypeFunc
        }
    }
}
