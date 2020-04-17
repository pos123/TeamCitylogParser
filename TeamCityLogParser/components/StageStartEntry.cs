﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamCityLogParser.Extractors;
using TeamCityLogParser.interfaces;

namespace TeamCityLogParser.components
{
    public class StageStartEntry : IStageStartType
    {
        private readonly IEntry entry;
        private readonly IValueExtractor valueExtractor;
        private readonly IDataService dataService;

        public StageStartEntry(IEntry entry, IValueExtractor valueExtractor, IDataService dataService)
        {
            this.entry = entry;
            this.valueExtractor = valueExtractor;
            this.dataService = dataService;
        }

        public EntryType EntryType => entry.EntryType;

        public uint LineNumber => entry.LineNumber;

        public TimeSpan Time =>
            valueExtractor.GetValueAsTimeSpan(entry.EntryType, "time", @"hh\:mm\:ss",
                dataService.Data(entry.LineNumber), TimeSpan.Zero);

        public uint StageNo => (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "start", dataService.Data(entry.LineNumber), 0);
        
        public uint StageCount => (uint)valueExtractor.GetValueAsNumber(entry.EntryType, "end", dataService.Data(entry.LineNumber), 0);

        public string StageLabel => valueExtractor.GetValueAsString(entry.EntryType, "stageLabel",
            dataService.Data(entry.LineNumber), string.Empty);
    }
}


