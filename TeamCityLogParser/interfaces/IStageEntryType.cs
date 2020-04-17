using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IStageEntryType : IEntry
    {
        uint StageNo { get; }
        uint StageCount { get; }
        TimeSpan Time { get; }
    }
}
