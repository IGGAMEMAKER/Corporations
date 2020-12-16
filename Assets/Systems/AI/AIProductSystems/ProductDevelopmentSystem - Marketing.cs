using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void ReleaseApp(GameEntity product)
    {
        // var Cluster = new SupportFeature { SupportBonus = new SupportBonusHighload(2_000_000), Name = "Cluster" };
        //
        // TryAddTask(product, new TeamTaskSupportFeature(Cluster));

        // Products.GetServerCapacity(product) > 2_000_000 &&
        if (Companies.IsReleaseableApp(product))
            Marketing.ReleaseApp(gameContext, product);
    }

    void ManageChannels(GameEntity product)
    {
        var channels = Markets.GetAffordableMarketingChannels(product, gameContext)
            .OrderBy(c => Marketing.GetChannelCostPerUser(product, c))
            .ThenByDescending(c => Marketing.GetChannelCost(product, c));
        ;

        if (channels.Any())
        {
            var c = channels.First();

            var cost = Marketing.GetChannelCost(product, c);
            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID, cost));
        }
        else
        {
            // maybe try to remove expensive ones if necessary?
        }
    }

    void GrabSegments(GameEntity product)
    {
        Companies.Log(product, "Need to grab more users");

        var positioning = Marketing.GetPositioning(product);
        var ourAudiences = Marketing.GetAmountOfTargetAudiences(positioning);

        var positionings = Marketing.GetNichePositionings(product);

        var audiences = Marketing.GetAudienceInfos();
        var maxAudiences = audiences.Count;

        var widerAudiencesCount = ourAudiences + 1;

        Companies.Log(product, $"Current positioning is {positioning.name} {string.Join(",", positioning.Loyalties)}");
        Companies.Log(product, $"Currently have {ourAudiences}, but need at least {widerAudiencesCount}");

        while (widerAudiencesCount <= maxAudiences)
        {
            var broaderPositionings = positionings.Where(p => Marketing.GetAmountOfTargetAudiences(p) == widerAudiencesCount);

            if (broaderPositionings.Count() == 0)
            {
                Companies.Log(product, $"No positionings for {widerAudiencesCount} audiences");

                widerAudiencesCount++;
                continue;
            }

            var newIndex = UnityEngine.Random.Range(0, broaderPositionings.Count());
            var newPositioning = broaderPositionings.ToArray()[newIndex];

            Companies.Log(product, $"positioning FOUND: {newPositioning.name}");

            // if positioning change will not dissapoint our current users...
            // change it to new one!
            foreach (var a in audiences)
            {
                var loyalty = Marketing.GetSegmentLoyalty(product, positioning, a.ID);
                var futureLoyalty = Marketing.GetSegmentLoyalty(product, newPositioning, a.ID);


                // will dissapoint loyal users
                if (loyalty > 0 && futureLoyalty < 0)
                {
                    Companies.Log(product, $"will dissapoint {a.Name}, whose loyalty is {loyalty} and after changing positioning will be {futureLoyalty}");

                    return;
                }
            }

            Companies.Log(product, $"POSITIONING SHIFTED TO: {newPositioning.name}!");

            Debug.Log($"POSITIONING of {product.company.Name} SHIFTED TO: {newPositioning.name}! {newPositioning.ID}");

            Marketing.ChangePositioning(product, gameContext, newPositioning.ID);

            return;
        }
    }
}
