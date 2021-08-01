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
        var channels = Markets.GetAffordableMarketingChannels(product, gameContext);

        if (!channels.Any())
            return;

        var c = channels.First();

        // TODO COPIED FROM MarketingChannelController.cs

        //TryAddTask(product, TeamTaskChannelActivity.FromChannel(c));
        var channelId = c.ID;
        var payer = Companies.GetPayer(product, gameContext);

        if (Marketing.IsActiveInChannel(product, channelId))
        {
            return;
        }

        if (Marketing.IsNeedsMoreMarketersForCampaign(product))
        {
            Debug.Log(product.company.Name + "needs to Hire more marketers!");
            return;
        }

        var task = TeamTaskChannelActivity.FromChannel(c); // new TeamTaskChannelActivity(channelId, Marketing.GetChannelCost(product, channelId));
        var cost = Teams.GetTaskCost(product, task, gameContext);

        if (Companies.IsEnoughResources(payer, cost))
        {
            Teams.AddTeamTask(product, ScheduleUtils.GetCurrentDate(gameContext), gameContext, 0, task);
        }
    }
}
