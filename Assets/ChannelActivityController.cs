using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelActivityController : ButtonController
{
    public MarketingChannelView MarketingChannelView;
    public override void Execute()
    {
        var channel = MarketingChannelView.channel;

        var companyId = Flagship.company.Id;
        var active = channel.companyMarketingActivities.Companies.ContainsKey(companyId);

        if (active)
            channel.companyMarketingActivities.Companies.Remove(companyId);
        else
            channel.companyMarketingActivities.Companies[companyId] = 1;

        MarketingChannelView.ViewRender();
    }
}
