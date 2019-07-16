using System;
using System.Text.RegularExpressions;
using TeamCityLogParser.interfaces;
using static System.String;

namespace TeamCityLogParser.Extractors
{
    public class ValueExtractor : IValueExtractor
    {
        private readonly IDataDictionary dictionary;

        public ValueExtractor(IDataDictionary dictionary)
        {
            this.dictionary = dictionary;
        }
        
        public string GetValueAsString(EntryType entryType, string label, string sourceData, string defaultValue)
        {
            var result = GetValue(entryType, label, sourceData);
            return result.Item1 ? result.Item2 : defaultValue;
        }

        public int GetValueAsNumber(EntryType entryType, string label, string sourceData, int defaultValue)
        {
            var result = GetValue(entryType, label, sourceData);
            if (!result.Item1) return defaultValue;
            int value;
            return int.TryParse(result.Item2, out value) 
                ? value 
                : defaultValue;
        }
        
        public bool IsMatchSuccess(EntryType entryType, string data)
        {
            return Regex.Match(data, GetPattern(entryType), RegexOptions.IgnoreCase).Success;
        }

        private string GetPattern(EntryType entryType)
        {
            return dictionary.GetDictionary().ContainsKey(entryType.Id) 
                ? dictionary.GetDictionary()[entryType.Id] 
                : Empty;
        }

        private Tuple<bool, string> GetValue(EntryType entryType, string label, string sourceData)
        {
            var pattern = GetPattern(entryType);
            var match = Regex.Match(sourceData, pattern, RegexOptions.IgnoreCase);
            return Tuple.Create(match.Success, match.Groups[label].Success ? match.Groups[label].Value : Empty);
        }
    }

}