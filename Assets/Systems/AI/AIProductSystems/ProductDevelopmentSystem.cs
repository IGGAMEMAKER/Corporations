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
        {
            List<string> str = new List<string>();

            if (Companies.IsReleaseableApp(product, gameContext))
                Marketing.ReleaseApp(gameContext, product);

            ManageSupport(product, ref str);

            ManageChannels(product, ref str);
            ManageFeatures(product, ref str);

            Investments.CompleteGoal(product, gameContext);
        }
    }

    void ManageFeatures(GameEntity product, ref List<string> str)
    {
        var remainingFeatures = Products.GetAvailableFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = remainingFeatures.First();
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

        TryAddTask(product, new TeamTaskFeatureUpgrade(feature), ref str);
    }

    void ManageSupport(GameEntity product, ref List<string> str)
    {
        int tries = 4;
        while (Products.IsNeedsMoreServers(product) && tries > 0)
        {
            tries--;
            AddServer(product, ref str);
        }

        var load = Products.GetServerLoad(product);
        var capacity = Products.GetServerCapacity(product);

        var growth = Marketing.GetAudienceChange(product, gameContext);

        if (load + growth >= capacity)
        {
            AddServer(product, ref str);
        }
    }

    void ManageChannels(GameEntity product, ref List<string> str)
    {
        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, false)
            .OrderBy(c => Marketing.GetChannelCostPerUser(product, gameContext, c));

        foreach (var c in channels)
        {
            var load = Products.GetServerLoad(product);
            var capacity = Products.GetServerCapacity(product);

            var growth = Marketing.GetAudienceChange(product, gameContext);

            var gain = Marketing.GetChannelClientGain(product, gameContext, c);
            bool willExceedLimits = load + growth + gain >= capacity;

            if (!willExceedLimits)
                TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID), ref str);
        }
    }

    void AddServer(GameEntity product, ref List<string> str)
    {
        var supportFeatures = Products.GetHighloadFeatures(product);
        var feature = supportFeatures[3];

        TryAddTask(product, new TeamTaskSupportFeature(feature), ref str);
    }

    // -----------------------------------------------------------------------------------

    bool CanMaintain(GameEntity product, long cost, ref List<string> str)
    {
        if (cost == 0)
            return true;

        return Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);
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
                if (teamTask.IsHighloadTask)
                {
                    Teams.AddTeam(product, TeamType.DevOpsTeam);
                }
                else
                {
                    Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
                }

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
