using System.Collections.Generic;
using System.Linq;
using Assets.Core;

public class ProcessClientsSystem : OnPeriodChange
{
    public ProcessClientsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var channels = Markets.GetMarketingChannels(gameContext);

        var audienceInfos = Marketing.GetAudienceInfos();

        //foreach (var c in channels)
        //{
        //    foreach (var companyId in c.channelMarketingActivities.Companies.Keys)
        //    {
        //        var company = Companies.Get(gameContext, companyId);

        //        foreach (var info in audienceInfos)
        //        {
        //            var gainedAudience = Marketing.GetChannelClientGain(company, gameContext, c);

        //            Marketing.AddClients(company, gainedAudience, company.productTargetAudience.SegmentId);
        //        }

        //    }
        //}

        var companies = Companies.GetProductCompanies(gameContext);

        foreach (var product in companies)
        {
            var myChannels = product.companyMarketingActivities.Channels;

            foreach (var info in audienceInfos)
            {
                var segmentId = info.ID;

                // churn users
                var churn = Marketing.GetChurnClients(gameContext, product.company.Id, segmentId);
                Marketing.AddClients(product, -churn, segmentId);

                // add users
                foreach (var c in myChannels)
                {
                    var channelId = c.Key;
                    var channel = channels[channelId];

                    var clients = Marketing.GetChannelClientGain(product, gameContext, channel, segmentId);
                    Marketing.AddClients(product, clients, segmentId);
                }
            }

        }
    }
}

