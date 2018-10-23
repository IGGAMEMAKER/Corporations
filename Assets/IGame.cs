using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGame
{
    bool PeriodTick(int count = 1);
    void StartAdCampaign(int projectId, int channelId);
    void PrepareAd(int projectId, int channelId, int duration);
    void UpgradeFeature(int projectId, int featureId);
    void ExploreFeature(int projectId, int featureId);
}
