using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Advert
    {
        public int AdEffeciency { get; set; }
        public int AdDuraion { get; set; }
        public bool IsRunningCampaign { get; set; }
        public int Channel { get; set; }
        public int Project { get; set; }

        public Advert(int channelId, int projectId, int adEffeciency = 0, int adDuration = 0)
        {
            AdEffeciency = adEffeciency;
            AdDuraion = adDuration;
            Channel = channelId;
            Project = projectId;
        }

        public void PrepareAd(int duration)
        {
            AdEffeciency = UnityEngine.Random.Range(Balance.advertEffeciencyRangeMin, Balance.advertEffeciencyRangeMax);
            AdDuraion = duration;
            IsRunningCampaign = false;
        }
    }
}
