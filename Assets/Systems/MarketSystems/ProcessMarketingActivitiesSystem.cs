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
            foreach (var pair in c.channelMarketingActivities.Companies)
            {
                var companyId = pair.Key;

                var company = Companies.Get(gameContext, companyId);

                var batch = c.marketingChannel.ChannelInfo.Batch;

                var marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, company, WorkerRole.MarketingLead);

                var gainedAudience = batch * (100 + marketingEffeciency) / 100;

                Marketing.AddClients(company, gainedAudience);
            }
        }
    }
}

