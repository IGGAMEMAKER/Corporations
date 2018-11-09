using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Advert: object
    {
        public int Effeciency { get; set; }
        public int Duration { get; set; }
        public bool IsRunningCampaign { get; set; }
        public int Channel { get; set; }
        public int Project { get; set; }

        public Advert(int channelId, int projectId, int adEffeciency = 0, int adDuration = 0)
        {
            Effeciency = adEffeciency;
            Duration = adDuration;
            Channel = channelId;
            Project = projectId;
        }

        public void PrepareAd(int duration)
        {
            Effeciency = UnityEngine.Random.Range(Balance.advertEffeciencyRangeMin, Balance.advertEffeciencyRangeMax);
            Duration = duration;
            IsRunningCampaign = false;
        }

        public bool NeedsPreparation
        {
            get
            {
                return Duration == 0;
            }
        }

        string FormatAd()
        {
            return String.Format("Advert of Project {0} on channel {1} with effeciency {2} and duration: {3}", Project, Channel, Effeciency, Duration);
        }

        public void Print()
        {
            Debug.LogFormat(FormatAd());
        }

        public override string ToString()
        {
            return String.Format("Advert of Project {0} on channel {1} with effeciency {2} and duration: {3}", Project, Channel, Effeciency, Duration);
        }
    }
}
