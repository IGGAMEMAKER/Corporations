using System.Collections.Generic;
using Assets.Core;

public class ProcessMarketingActivitiesSystem : OnPeriodChange
{
    public ProcessMarketingActivitiesSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var channels = Markets.GetMarketingChannels(gameContext);

        foreach (var c in channels)
        {
            foreach (var companyId in c.channelMarketingActivities.Companies.Keys)
            {
                var company = Companies.Get(gameContext, companyId);

                var gainedAudience = Marketing.GetChannelClientGain(company, gameContext, c);

                Marketing.AddClients(company, gainedAudience, company.productTargetAudience.SegmentId);
            }
        }
    }
}

