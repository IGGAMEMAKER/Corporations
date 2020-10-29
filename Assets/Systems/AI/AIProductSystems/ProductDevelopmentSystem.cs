using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ProductActions
{
    Features,
    GrabUsers,
    ExpandTeam, // hire more people, add more teams
    HandleTeam,

    ReleaseApp,

    Monetise,

    GrabSegments,

    ShowProfit
}

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var nonFlagshipProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isFlagship);

        foreach (var product in nonFlagshipProducts)
        {
            List<ProductActions> actions = new List<ProductActions>();

            Companies.Log(product, "Product development system");

            // add goal if there are no goals
            if (product.companyGoal.Goals.Count == 0)
            {
                var pickableGoals = Investments.GetNewGoals(product, gameContext);

                var max = pickableGoals.Count;

                if (max == 0)
                {
                    Companies.Log(product, "CANNOT GET A GOAL");

                    Debug.LogError("CANNOT GET A GOAL FOR: " + product.company.Name);

                    continue;
                }

                Investments.AddCompanyGoal(product, gameContext, pickableGoals[UnityEngine.Random.Range(0, max)]);
            }

            var goal = product.companyGoal.Goals.FirstOrDefault();
            Companies.Log(product, "Achieving goal: " + goal.GetFormattedName());

            switch (goal.InvestorGoalType)
            {
                case InvestorGoalType.ProductPrototype:
                    actions.Add(ProductActions.Features);

                    break;

                case InvestorGoalType.ProductFirstUsers:
                    actions.Add(ProductActions.GrabUsers);
                    actions.Add(ProductActions.Features);

                    break;

                case InvestorGoalType.ProductBecomeMarketFit:
                    actions.Add(ProductActions.Features);

                    break;

                case InvestorGoalType.ProductRelease:
                    actions.Add(ProductActions.ExpandTeam);
                    actions.Add(ProductActions.ReleaseApp);

                    break;

                case InvestorGoalType.StartMonetising:
                    actions.Add(ProductActions.Monetise);

                    break;

                case InvestorGoalType.GainMoreSegments:
                    actions.Add(ProductActions.GrabSegments);
                    actions.Add(ProductActions.Features);
                    actions.Add(ProductActions.GrabUsers);

                    break;

                case InvestorGoalType.GrowUserBase:
                    actions.Add(ProductActions.GrabUsers);
                    actions.Add(ProductActions.Features);

                    break;

                case InvestorGoalType.GrowIncome:
                    actions.Add(ProductActions.Features);
                    actions.Add(ProductActions.GrabUsers);

                    break;

                case InvestorGoalType.BecomeProfitable:
                    actions.Add(ProductActions.GrabUsers);
                    actions.Add(ProductActions.ShowProfit);
                    actions.Add(ProductActions.Features);
                    actions.Add(ProductActions.ExpandTeam);

                    break;

                default:
                    Companies.Log(product, "No specific actions for current goal");

                    actions.Add(ProductActions.GrabUsers);
                    actions.Add(ProductActions.Features);

                    break;
            }

            foreach (var action in actions)
            {
                ManageProduct(action, product);
            }

            Investments.CompleteGoal(product, gameContext, goal);

            Investments.CompleteGoals(product, gameContext);
        }
    }

    private void ManageProduct(ProductActions action, GameEntity product)
    {
        Companies.Log(product, "Manage: " + action);
        switch (action)
        {
            case ProductActions.Features:
                ManageFeatures(product);
                break;

            case ProductActions.ReleaseApp:
                ReleaseApp(product);
                break;

            case ProductActions.Monetise:
                //Monetise(product);
                ManageFeatures(product);
                break;

            case ProductActions.GrabUsers:
                ManageChannels(product);
                break;

            case ProductActions.GrabSegments:
                GrabSegments(product);
                break;

            case ProductActions.ExpandTeam:
                ExpandTeam(product);
                break;
        }
    }

    void TryAddTeam(GameEntity product, TeamType teamType)
    {
        var teamCost = Economy.GetSingleTeamCost();

        if (CanMaintain(product, teamCost) && !product.team.Teams.Any(t => t.TeamType == teamType))
        {
            var id = Teams.AddTeam(product, teamType);

            if (teamType == TeamType.DevelopmentTeam)
                SetTasks(product, ManagerTask.Polishing, id);

            if (teamType == TeamType.MarketingTeam)
                SetTasks(product, ManagerTask.ViralSpread, id);

            if (teamType == TeamType.ServersideTeam)
                SetTasks(product, ManagerTask.Organisation, id);
        }
    }

    void SetTasks(GameEntity product, ManagerTask managerTask, int teamId)
    {
        Teams.SetManagerTask(product, teamId, 0, managerTask);
        Teams.SetManagerTask(product, teamId, 1, managerTask);
        Teams.SetManagerTask(product, teamId, 2, managerTask);
    }

    void ExpandTeam(GameEntity product)
    {
        if (product.team.Teams.Count < 4)
        {
            TryAddTeam(product, TeamType.DevelopmentTeam);
            TryAddTeam(product, TeamType.MarketingTeam);
            TryAddTeam(product, TeamType.ServersideTeam);
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

        while (widerAudiencesCount < maxAudiences)
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
                    return;
            }

            Companies.Log(product, $"POSITIONING SHIFTED TO: {newPositioning.name}!");
            Marketing.ChangePositioning(product, newPositioning.ID);

            return;
        }
    }

    void ReleaseApp(GameEntity product)
    {
        var goal = product.companyGoal.Goals.First(g => g.InvestorGoalType == InvestorGoalType.ProductRelease);
        var requirements = goal.GetGoalRequirements(product, gameContext);

        if (Companies.IsReleaseableApp(product))
            Marketing.ReleaseApp(gameContext, product);
    }

    void Monetise(GameEntity product)
    {
        ManageFeatures(product);
    }

    void ManageFeatures(GameEntity product)
    {
        var remainingFeatures = Products.GetAllFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = remainingFeatures.First();
        if (feature.FeatureBonus.isMonetisationFeature) //  && feature.Name.Contains("Ads")
        {
            var segments = Marketing.GetAudienceInfos();

            foreach (var s in segments)
            {
                var loyalty = Marketing.GetSegmentLoyalty(product, s.ID);
                var attitude = feature.AttitudeToFeature[s.ID];

                // will make audience sad
                if (loyalty + attitude < 0)
                    return;
            }
        }

        TryAddTask(product, new TeamTaskFeatureUpgrade(feature));
    }

    void ManageSupport(GameEntity product)
    {
        int tries = 2;
        if (Products.IsNeedsMoreServers(product) && tries > 0)
        {
            tries--;
            AddServer(product);
        }

        var load = Products.GetServerLoad(product);
        var capacity = Products.GetServerCapacity(product);

        var growth = Marketing.GetAudienceChange(product, gameContext);

        if (load + growth >= capacity)
        {
            AddServer(product);
        }
    }

    void ManageChannels(GameEntity product)
    {
        var channels = Markets.GetAffordableMarketingChannels(product, gameContext)
            .OrderBy(c => Marketing.GetChannelCostPerUser(product, gameContext, c))
            ;

        if (channels.Count() > 0)
        {
            var c = channels.First();
        
            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID, Marketing.GetChannelCost(product, c)));
        }
    }

    void AddServer(GameEntity product)
    {
        var supportFeatures = Products.GetHighloadFeatures(product);
        var feature = supportFeatures[3];

        TryAddTask(product, new TeamTaskSupportFeature(feature));
    }

    // -----------------------------------------------------------------------------------

    bool CanMaintain(GameEntity product, long cost)
    {
        if (cost == 0)
            return true;

        return Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);
    }

    void TryAddTask(GameEntity product, TeamTask teamTask)
    {
        var taskCost = Economy.GetTeamTaskCost(product, teamTask);

        if (!CanMaintain(product, taskCost))
            return;

        // searching team for this task
        int teamId = 0; // Teams.GetTeamIdForTask(product, teamTask);

        if (teamId == -1)
        {
            // need to hire new team
            var teamCost = Economy.GetSingleTeamCost();

            if (CanMaintain(product, teamCost + taskCost))
            {
                if (teamTask.IsHighloadTask)
                {
                    Teams.AddTeam(product, TeamType.ServersideTeam);
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
}
