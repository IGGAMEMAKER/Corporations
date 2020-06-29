using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

        foreach (var product in Companies.GetProductCompanies(gameContext))
        {
            if (product.company.Id != playerFlagshipId)
                ManageProduct(product);
        }
    }

    void ManageProduct(GameEntity product)
    {
        List<string> str = new List<string>();

        if (Companies.IsReleaseableApp(product, gameContext))
        {
            Marketing.ReleaseApp(gameContext, product);
        }

        var balance = Economy.BalanceOf(product);


        var income = Economy.GetCompanyIncome(gameContext, product);
        var maintenance = Economy.GetCompanyMaintenance(gameContext, product);


        var managerMaintenance = Economy.GetManagersCost(product, gameContext);
        var totalFunds = balance + income - managerMaintenance;

        str.Add($"------------------ {product.company.Name} (#{product.creationIndex}) -------------------");
        str.Add($"Balance: " + Format.Money(balance) + " (Income: " + Visuals.Positive("+" + Format.Money(income)) + " Expenses: " + Visuals.Negative("-" + Format.Money(maintenance)) + ")");
        //str.Add("Money available: " + Visuals.PositiveOrNegativeMinified(totalFunds));

        // features (free)
        // servers and support
        // marketing channels (growth)

        //ManageFeatures(product, ref str, ref totalFunds);
        //ManageSupport(product, ref str, ref totalFunds);
        ManageChannels(product, ref str, ref totalFunds);

        bool isTestCompany = product.company.Name == "Money Exchange 0"; // IsInPlayerSphereOfInterest(product)
        if (isTestCompany)
        {
            foreach (var s in str)
                Debug.Log(s);
        }
    }

    bool IsUniversal(TeamType teamType) => new TeamType[] { TeamType.BigCrossfunctionalTeam, TeamType.CoreTeam, TeamType.CrossfunctionalTeam, TeamType.SmallCrossfunctionalTeam }.Contains(teamType);

    bool SupportsTeamTask(TeamType teamType, TeamTask teamTask)
    {
        if (IsUniversal(teamType))
            return true;

        if (teamTask.IsFeatureUpgrade())
            return teamType == TeamType.DevelopmentTeam;

        if (teamTask.IsMarketingTask())
            return teamType == TeamType.MarketingTeam;

        if (teamTask.IsSupportTask())
            return teamType == TeamType.SupportTeam;

        if (teamTask.IsHighloadTask())
            return teamType == TeamType.DevOpsTeam;

        return false;
    }

    bool IsInPlayerSphereOfInterest(GameEntity product) => Companies.IsInPlayerSphereOfInterest(product, gameContext);

    bool CanMaintain(GameEntity product, long cost, ref List<string> str, ref long totalFunds)
    {
        var result = Economy.IsCanMaintain(product, gameContext, cost); // Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);

        //str.Add(product.company.Name + " Can Maintain " + Format.MinifyMoney(cost) + $" ? {result}");

        return result;
    }

    void TryAddTask(GameEntity product, TeamTask teamTask, ref List<string> str, ref long totalFunds)
    {
        // TODO
        // CHECK TASK SELF COST!!!

        str.Add($"-- Trying to add task {teamTask.GetTaskName()} to {product.company.Name} --");

        var taskCost = Economy.GetTeamTaskCost(product, gameContext, teamTask);
        if (!CanMaintain(product, taskCost, ref str, ref totalFunds))
        {
            str.Add("-- " + Visuals.Negative("This task is too expensive!") + $" Need {Format.MinifyMoney(taskCost)}  --");

            return;
        }

        int teamId = 0;
        foreach (var t in product.team.Teams)
        {
            if (SupportsTeamTask(t.TeamType, teamTask) && t.Tasks.Count < C.TASKS_PER_TEAM)
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

        if (CanMaintain(product, teamCost + taskCost, ref str, ref totalFunds))
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

    void ManageFeatures(GameEntity product, ref List<string> str, ref long totalFunds)
    {
        var remainingFeatures = Products.GetAvailableFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = new TeamTaskFeatureUpgrade(remainingFeatures.First());

        TryAddTask(product, feature, ref str, ref totalFunds);
    }

    void ManageSupport(GameEntity product, ref List<string> str, ref long totalFunds)
    {
        if (Products.IsNeedsMoreServers(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetServerCapacity(product);

            var supportFeatures = Products.GetHighloadFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature), ref str, ref totalFunds);
        }

        if (Products.IsNeedsMoreMarketingSupport(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetSupportCapacity(product);

            var supportFeatures = Products.GetMarketingSupportFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature), ref str, ref totalFunds);
        }
    }


    void ManageChannels(GameEntity product, ref List<string> str, ref long totalFunds)
    {
        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, false);

        channels
            .Where(c => !Marketing.IsCompanyActiveInChannel(product, c))
            .OrderByDescending(c => Marketing.GetChannelROI(product, gameContext, c));

        foreach (var c in channels)
        {
            var cost = Marketing.GetMarketingActivityCost(product, gameContext, c);

            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID), ref str, ref totalFunds);
            //totalFunds = CheckChannelCosts(product, c, totalFunds, ref str);
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
}
