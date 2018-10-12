using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Task
    {
        public TaskType type { get; set; }
        public int StartDate;
        public int FinishDate;
        public int Duration;

        public Dictionary<string, object> Parameters;

        int Progress;

        public Task (TaskType taskType, int duration, int progress, Dictionary<string, object> parameters, int startDate, int finishDate)
        {
            type = taskType;
            Duration = duration;
            Progress = progress;
            Parameters = parameters;
            StartDate = startDate;
            FinishDate = finishDate;
        }

        public enum TaskType
        {
            UpgradeFeature,
            ExploreFeature,
            MakeAd,
            StartAdCampaign,
            RaiseInvestments
        }

        internal void Tick()
        {
            Progress--;
        }

        public bool IsFinished()
        {
            return Progress <= 0;
        }
    }
}
