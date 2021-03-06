using System;
using System.Collections.Generic;
using System.Linq;
using TeamCityLogParser.interfaces;
using static System.String;

namespace TeamCityLogParser
{
    public class DataService : IDataService
    {
        private readonly List<string> data;
        
        public DataService(string text)
        {
            if (IsNullOrEmpty(text))
            {
                throw new NullReferenceException("supplied data cannot be empty or null");
            }
            
            data = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        public string Data(uint lineNumber)
        {
            return data.Count >= lineNumber ? data[(int) (lineNumber - 1)] : Empty;
        }

        public IEnumerable<Tuple<uint,string>> Data()
        {
            uint lineNumber = 0;
            foreach (var line in data)
            {
                yield return Tuple.Create(++lineNumber, line);
            }
        }

        public IEnumerable<Tuple<uint, string>> FilteredData(uint start, uint end)
        {
            uint lineNumber = 0;
            foreach (var line in data)
            {
                ++lineNumber;
                if (lineNumber >= start && lineNumber <= end)
                {
                    yield return Tuple.Create(lineNumber, line);
                }

                if (lineNumber > end)
                {
                    yield break;
                }
            }
        }
    }
}