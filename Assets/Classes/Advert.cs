namespace Assets.Classes
{
    public class Advert
    {
        public int AdEffeciency { get; set; }
        public int AdDuraion { get; set; }
        public bool IsRunningCampaign { get; set; }
        public int Channel { get; set; }

        public Advert(int channelId, int adEffeciency = 0, int adDuration = 0)
        {
            AdEffeciency = adEffeciency;
            AdDuraion = adDuration;
            Channel = channelId;
        }

        public void PrepareAd(int duration)
        {
            AdEffeciency = UnityEngine.Random.Range(Balance.advertEffeciencyRangeMin, Balance.advertEffeciencyRangeMax);
            AdDuraion = duration;
            IsRunningCampaign = false;
        }
    }
}
