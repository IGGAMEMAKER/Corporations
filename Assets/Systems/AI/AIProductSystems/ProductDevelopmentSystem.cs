using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var nonFlagshipProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isFlagship);

        foreach (var product in nonFlagshipProducts)
            ManageProduct(product);
    }

    void ManageProduct(GameEntity product)
    {
        List<string> str = new List<string>();

        if (Companies.IsReleaseableApp(product, gameContext))
            Marketing.ReleaseApp(gameContext, product);

        ManageChannels(product, ref str);
        ManageFeatures(product, ref str);
        ManageSupport(product, ref str);

        Investments.CompleteGoal(product, gameContext);
    }

    void ManageFeatures(GameEntity product, ref List<string> str)
    {
        var remainingFeatures = Products.GetAvailableFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;


        var feature = remainingFeatures.First();
        var teamTask = new TeamTaskFeatureUpgrade(feature);

        if (feature.FeatureBonus.isMonetisationFeature) //  && feature.Name.Contains("Ads")
        {
            var segments = Marketing.GetAudienceInfos();

            foreach (var s in segments)
            {
                var loyalty = Marketing.GetSegmentLoyalty(gameContext, product, s.ID);
                var attitude = feature.AttitudeToFeature[s.ID];

                // will make audience sad
                if (loyalty + attitude < 0)
                    return;
            }
        }

        TryAddTask(product, teamTask, ref str);
    }

    void ManageSupport(GameEntity product, ref List<string> str)
    {
        if (Products.IsNeedsMoreServers(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetServerCapacity(product);

            var supportFeatures = Products.GetHighloadFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature), ref str);
        }

        //if (Products.IsNeedsMoreMarketingSupport(product))
        //{
        //    var diff = Products.GetClientLoad(product) - Products.GetSupportCapacity(product);

        //    var supportFeatures = Products.GetMarketingSupportFeatures(product);
        //    var feature = supportFeatures[2];

        //    TryAddTask(product, new TeamTaskSupportFeature(feature), ref str);
        //}
    }

    void ManageChannels(GameEntity product, ref List<string> str)
    {
        var targetAudience = product.productTargetAudience.SegmentId;

        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, false)
            .OrderBy(c => Marketing.GetChannelCostPerUser(product, gameContext, c));

        //if (channels.Count() > 0)
            foreach (var c in channels)
            {
            //TryAddTask(product, new TeamTaskChannelActivity(channels.First().marketingChannel.ChannelInfo.ID), ref str);
            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID), ref str);
        }
    }

    

    bool IsInPlayerSphereOfInterest(GameEntity product) => Companies.IsInPlayerSphereOfInterest(product, gameContext);

    bool CanMaintain(GameEntity product, long cost, ref List<string> str)
    {
        if (cost == 0)
            return true;

        var result = Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);

        return result;
    }

    void TryAddTask(GameEntity product, TeamTask teamTask, ref List<string> str)
    {
        var taskCost = Economy.GetTeamTaskCost(product, gameContext, teamTask);

        if (!CanMaintain(product, taskCost, ref str))
            return;

        // searching team for this task
        int teamId = Teams.GetTeamIdForTask(product, teamTask);

        if (teamId == -1)
        {
            // need to hire new team
            var teamCost = Economy.GetSingleTeamCost();

            if (CanMaintain(product, teamCost + taskCost, ref str))
            {
                Teams.AddTeam(product, TeamType.CrossfunctionalTeam);

                teamId = product.team.Teams.Count - 1;
            }
        }

        if (teamId >= 0)
        {
            Teams.AddTeamTask(product, gameContext, teamId, teamTask);
        }
    }

    void Print(string txt, ref List<string> str)
    {
        str.Add(txt);
    }
}
