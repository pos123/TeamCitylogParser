namespace TeamCityLogParser.interfaces
{
    public interface ISucceedFailedSkipped
    {
        uint Succeeded { get;  }
        uint Failed { get;  }
        uint Skipped { get;  }
    }
}