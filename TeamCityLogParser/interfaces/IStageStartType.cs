using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IStageStartType : IStageEntryType
    {
        string StageLabel { get; }
    }
}
