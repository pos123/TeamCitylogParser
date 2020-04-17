using System;
using System.Collections.Generic;

namespace TeamCityLogParser.interfaces
{
    public interface IDataService
    {
        string Data(uint lineNumber);
        IEnumerable<Tuple<uint, string>> Data();
        IEnumerable<Tuple<uint, string>> FilteredData(uint start, uint end);
    }
}