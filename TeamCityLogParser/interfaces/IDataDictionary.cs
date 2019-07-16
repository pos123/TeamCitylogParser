using System.Collections.Generic;

namespace TeamCityLogParser.interfaces
{
    public interface IDataDictionary
    {
        Dictionary<uint, string> GetDictionary();
    }
}