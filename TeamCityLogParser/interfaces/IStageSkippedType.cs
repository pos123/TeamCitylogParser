using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IStageSkippedType : IStageEntryType
    {
        string Label { get; }
    }
}
