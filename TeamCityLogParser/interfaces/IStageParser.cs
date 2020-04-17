using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityLogParser.interfaces
{
    public interface IStageParser
    {
        Task Parse(uint lineStart, uint lineEnd, Action<string> notification);
        Tuple<bool, string> GetStatement();
    }
}
