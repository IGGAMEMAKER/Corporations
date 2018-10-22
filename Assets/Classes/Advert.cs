using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Advert: object
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

        string FormatAd()
        {
            return String.Format("Advert of Project {0} on channel {1} with effeciency {2} and duration: {3}", Project, Channel, AdEffeciency, AdDuraion);
        }

        public void Print()
        {
            Debug.LogFormat(FormatAd());
        }

        public override string ToString()
        {
            return String.Format("Advert of Project {0} on channel {1} with effeciency {2} and duration: {3}", Project, Channel, AdEffeciency, AdDuraion);
        }
    }
}
