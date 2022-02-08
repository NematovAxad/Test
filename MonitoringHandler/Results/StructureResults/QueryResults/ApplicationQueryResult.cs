using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Results.StructureResults.QueryResults
{
    public class ApplicationQueryResult
    {
        public int Count { get; set; }
        public List<object> Data { get; set; }
        public Statistics Statistics { get; set; }
    }
    public class Statistics
    {
        public int Done { get; set; }
        public int InProgress { get; set; }
        public int NotDone { get; set; }
        public int ForApproval { get; set; }
        public int ExecutedWithDelay { get; set; }
        public int Canceled { get; set; }
        public int FinalStage { get; set; }
        public int ProjectsCount { get; set; }
    }
}
