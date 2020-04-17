using System;
using System.Collections.Generic;
using System.Text;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser
{
    public class StageGroup
    {
        public IStageStartType StageStart { get; set; }

        public IStageExitType StageExit { get; set; }
        
        public StageGroupType StageGroupType { get; set; }

        public bool IsStageCompleted => StageStart != null && StageExit != null;
        
        public bool IsStageSuccess => IsStageCompleted && StageExit.Succeeded;
        
        public bool IsStageFailure => IsStageCompleted && !StageExit.Succeeded;

        public uint GroupStageNo => StageStart.StageNo;

        public TimeSpan StageTime => StageExit.Time - StageStart.Time;

        public Tuple<uint, uint> StageLineRange => IsStageCompleted
            ? new Tuple<uint, uint>(StageStart.LineNumber, StageExit.LineNumber)
            : new Tuple<uint, uint>(0, 0);
    }
}
