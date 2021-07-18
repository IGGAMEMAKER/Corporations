using System.Collections.Generic;
using Assets.Core;

public class ProcessClientsSystem : OnPeriodChange
{
    public ProcessClientsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var audiences = Marketing.GetAudienceInfos();

        var companies = Companies.GetProductCompanies(gameContext);

        foreach (var product in companies)
        {
            var myChannels = product.companyMarketingActivities.Channels;

            foreach (var info in audiences)
            {
                var segmentId = info.ID;

                // churn users
                var churn = Marketing.GetChurnClients(product, gameContext, segmentId);
                Marketing.AddClients(product, -churn, segmentId);

                // add users
                foreach (var c in myChannels)
                {
                    var channelId = c.Key;

                    var clients = Marketing.GetChannelClientGain(product, channelId, segmentId);
                    Marketing.AddClients(product, clients, segmentId);
                }
            }
        }
    }
}

