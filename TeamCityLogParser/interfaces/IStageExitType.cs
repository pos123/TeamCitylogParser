using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IStageExitType : IStageEntryType
    {
        bool Succeeded { get; }
    }
}
