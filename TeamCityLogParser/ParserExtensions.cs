using System;
using System.Collections.Generic;
using System.Linq;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    
    public static class ParserExtensions
    {
        public static IEnumerable<Func<Tuple<uint,EntryType>>> MapCalcEntryTypeFunc(
            this IEnumerable<Tuple<uint, string>> source, 
            IValueExtractor valueExtractor, Dictionary<uint, EntryType> entryTypes)
        {
            foreach (var (item1, item2) in source)
            {
                yield return () =>
                {
                    var candidates = new List<Tuple<uint, EntryType>> {Tuple.Create(item1, EntryType.Noise())};
                    foreach (var pair in entryTypes)
                    {
                        if (valueExtractor.IsMatchSuccess(pair.Value, item2))
                        {
                            candidates.Add(Tuple.Create<uint, EntryType>(item1, pair.Value));
                        }
                    }
                    return candidates.OrderBy(x => x.Item2.Priority).First();
                };
            }
        }
        
        public static IEnumerable<Func<IValueExtractor, IDataService, TIReturnType>> FilterEntryDefinitionTypeFunc<TIReturnType>(
            this IEnumerable<Func<Tuple<uint,EntryType>>> sourceEntryTypeFunc, 
            Func<uint, Func<IValueExtractor, IDataService, TIReturnType>> concreteTypeFunc, 
            EntryType comparisonType)
        {
            foreach (var func in sourceEntryTypeFunc)
            {
                var (item1, item2) = func();
                if (item2.Id == comparisonType.Id)
                {
                    yield return concreteTypeFunc(item1);
                }
            }
        }
        
        public static List<TIReturn> EvaluateToList<TIReturn>(
            this IEnumerable<Func<IValueExtractor, IDataService, TIReturn>> evaluations, IValueExtractor valueExtractor,
            IDataService dataService)
        {
            return evaluations.Select(func => func(valueExtractor, dataService)).ToList();
        }
        
    }
}