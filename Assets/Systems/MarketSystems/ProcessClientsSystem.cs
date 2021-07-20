using System.Collections.Generic;
using Assets.Core;

public class ProcessClientsSystem : OnPeriodChange
{
    public ProcessClientsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = Companies.GetProductCompanies(gameContext);

        foreach (var product in companies)
        {
            // churn users
            var churn = Marketing.GetChurnClients(product, gameContext);
            Marketing.AddClients(product, -churn);

            // add users
            if (product.isControlledByPlayer || product.isRelatedToPlayer)
                continue;

            var myChannels = product.companyMarketingActivities.Channels;

            foreach (var c in myChannels)
            {
                var channelId = c.Key;

                var clients = Marketing.GetChannelClientGain(product, channelId);
                Marketing.AddClients(product, clients);
            }
        }
    }
}

