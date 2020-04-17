using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IDefaultStageParserResult
    {
        List<Tuple<uint, string>> GetErrors();
    }
}
