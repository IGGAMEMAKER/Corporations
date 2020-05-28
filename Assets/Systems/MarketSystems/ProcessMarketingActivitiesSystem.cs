using System.Collections.Generic;
using Assets.Core;

public partial class ProcessMarketingActivitiesSystem : OnPeriodChange
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

                var gainedAudience = c.marketingChannel.ChannelInfo.Batch;

                var company = Companies.Get(gameContext, companyId);

                Marketing.AddClients(company, gainedAudience);
            }
        }
    }
}
