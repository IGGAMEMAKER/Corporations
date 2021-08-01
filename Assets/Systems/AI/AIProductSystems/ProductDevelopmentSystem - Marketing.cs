using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void ReleaseProduct(GameEntity product)
    {
        if (Companies.IsReleaseableApp(product))
            Marketing.ReleaseApp(gameContext, product);
    }

    void ManageChannels(GameEntity product)
    {
        // TODO COPIED FROM MarketingChannelController.cs
        if (Marketing.IsNeedsMoreMarketersForCampaign(product))
        {
            TryHireWorker(product, WorkerRole.Marketer);

            Debug.Log(product.company.Name + "needs to Hire more marketers!");
            return;
        }

        //var channels = Markets.GetAffordableMarketingChannels(product, gameContext);
        var channels = Markets.GetMaintainableMarketingChannels(product, gameContext)
            .Where(c => !Marketing.IsActiveInChannel(product, c.ID))
            .OrderByDescending(c => c.Batch)
            .Take(8);

        if (!channels.Any())
            return;

        var channel = channels.First();

        var task = TeamTaskChannelActivity.FromChannel(channel); // new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(product, channelId));

        Teams.AddTeamTask(product, ScheduleUtils.GetCurrentDate(gameContext), gameContext, 0, task);
    }
}
