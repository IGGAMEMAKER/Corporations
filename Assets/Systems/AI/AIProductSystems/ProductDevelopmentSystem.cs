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
        {
            Marketing.ReleaseApp(gameContext, product);
        }

        ManageFeatures(product, ref str);
        ManageSupport(product, ref str);
        ManageChannels(product, ref str);
    }

    void ManageFeatures(GameEntity product, ref List<string> str)
    {
        var remainingFeatures = Products.GetAvailableFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = new TeamTaskFeatureUpgrade(remainingFeatures.First());

        TryAddTask(product, feature, ref str);
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

        if (Products.IsNeedsMoreMarketingSupport(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetSupportCapacity(product);

            var supportFeatures = Products.GetMarketingSupportFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature), ref str);
        }
    }

    void ManageChannels(GameEntity product, ref List<string> str)
    {
        var targetAudience = product.productTargetAudience.SegmentId;

        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, false)
            //.Where(c => !Marketing.IsCompanyActiveInChannel(product, c))
            .Where(c => Marketing.IsChannelProfitable(product, gameContext, c, targetAudience))
            .OrderBy(c => Marketing.GetChannelRepaymentSpeed(product, gameContext, c, targetAudience));

        foreach (var c in channels)
        {
            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID), ref str);
        }
    }


    bool IsUniversal(TeamType teamType) => new TeamType[] { TeamType.BigCrossfunctionalTeam, TeamType.CoreTeam, TeamType.CrossfunctionalTeam, TeamType.SmallCrossfunctionalTeam }.Contains(teamType);

    bool SupportsTeamTask(TeamType teamType, TeamTask teamTask)
    {
        if (IsUniversal(teamType))
            return true;

        if (teamTask.IsFeatureUpgrade)
            return teamType == TeamType.DevelopmentTeam;

        if (teamTask.IsMarketingTask)
            return teamType == TeamType.MarketingTeam;

        if (teamTask.IsSupportTask)
            return teamType == TeamType.SupportTeam;

        if (teamTask.IsHighloadTask)
            return teamType == TeamType.DevOpsTeam;

        return false;
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
        // TODO
        // CHECK TASK SELF COST!!!

        str.Add($"-- Trying to add task {teamTask.GetTaskName()} to {product.company.Name} --");

        var taskCost = Economy.GetTeamTaskCost(product, gameContext, teamTask);
        var tooExpensive = !CanMaintain(product, taskCost, ref str);

        if (tooExpensive)
        {
            str.Add("-- " + Visuals.Negative("This task is too expensive!") + $" Need {Format.MinifyMoney(taskCost)}  --");

            return;
        }

        // searching team for this task
        int teamId = 0;
        foreach (var t in product.team.Teams)
        {
            var hasFreeSlot = t.Tasks.Count < C.TASKS_PER_TEAM;
            var teamCanDoThisTask = SupportsTeamTask(t.TeamType, teamTask);

            if (teamCanDoThisTask && hasFreeSlot)
            {
                Teams.AddTeamTask(product, gameContext, teamId, teamTask);
                str.Add($"-- Added task to <b>existing</b> team[{teamId}]: " + teamTask.ToString() + " --");

                return;
            }

            teamId++;
        }

        str.Add($"<b>No team found for this task</b>. {product.team.Teams.Count} available");


        // need to hire new team
        // if has money

        var teamCost = Economy.GetSingleTeamCost();

        if (CanMaintain(product, teamCost + taskCost, ref str))
        {
            Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
            str.Add($"Added team to <b>{product.company.Name}</b>");

            Teams.AddTeamTask(product, gameContext, product.team.Teams.Count - 1, teamTask);

            str.Add("Added team task after adding team to " + product.company.Name);
            str.Add("-- " + Visuals.Positive("Task added successfully") + " --");
        }
        else
        {
            str.Add("-- " + Visuals.Negative("Not enough money to add task AND team!") + $" Need {Format.MinifyMoney(teamCost + taskCost)}  --");
        }
    }

    long CheckChannelCosts(GameEntity product, GameEntity channel, long balance, ref List<string> str)
    {
        var newBalance = balance;

        var cost = Marketing.GetMarketingActivityCost(product, gameContext, channel);
        var workerCost = 0; // Products.GetUpgradeWorkerCost(product, gameContext, u);

        var totalCost = cost + workerCost;

        if (totalCost < newBalance)
        {
            Marketing.EnableChannelActivity(product, gameContext, channel);

            newBalance -= totalCost;
        }

        else
        {
            Marketing.DisableChannelActivity(product, gameContext, channel);
        }

        return newBalance;
    }

    void Print(string txt, ref List<string> str)
    {
        str.Add(txt);
    }
}
