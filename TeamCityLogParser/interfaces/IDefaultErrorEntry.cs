using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCityLogParser.interfaces
{
    public interface IDefaultErrorEntry : IEntry
    {
        string Error { get; }
    }
}
