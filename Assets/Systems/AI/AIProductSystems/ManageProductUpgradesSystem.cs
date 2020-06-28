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
        str.Add($"------------------ {product.company.Name} (#{product.creationIndex}) -------------------");
        str.Add($"Balance: " + Format.Money(balance));

        var income = Economy.GetCompanyIncome(gameContext, product);
        var maintenance = Economy.GetCompanyMaintenance(gameContext, product);
        str.Add("Income: " + Format.Money(income));
        str.Add("Total Expenses: " + Format.Money(maintenance));

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);
        var totalFunds = balance + income - managerMaintenance;
        str.Add("Money available for upgrades: " + Format.Money(totalFunds));

        // features (free)
        // servers and support
        // marketing channels (growth)

        ManageFeatures(product, ref str, ref totalFunds);
        ManageSupport(product, ref str, ref totalFunds);
        ManageChannels(product, ref str, ref totalFunds);

        if (IsInPlayerSphereOfInterest(product))
        {
            foreach (var s in str)
                Debug.Log(s);
        }
    }

    bool IsUniversal(TeamType teamType) => new TeamType[] { TeamType.BigCrossfunctionalTeam, TeamType.CoreTeam, TeamType.CrossfunctionalTeam, TeamType.SmallCrossfunctionalTeam }.Contains(teamType);

    bool SupportsTeamTask(TeamType teamType, TeamTask teamTask)
    {
        if (teamTask is TeamTaskFeatureUpgrade)
            return IsUniversal(teamType) || teamType == TeamType.DevelopmentTeam;

        if (teamTask is TeamTaskChannelActivity)
            return IsUniversal(teamType) || teamType == TeamType.MarketingTeam;

        if (teamTask is TeamTaskSupportFeature)
        {
            if ((teamTask as TeamTaskSupportFeature).SupportFeature.SupportBonus is SupportBonusHighload)
                return IsUniversal(teamType) || teamType == TeamType.DevOpsTeam;

            return IsUniversal(teamType) || teamType == TeamType.SupportTeam;
        }

        return false;
    }

    bool IsInPlayerSphereOfInterest(GameEntity product) => Companies.IsInPlayerSphereOfInterest(product, gameContext);

    bool CanMaintain(GameEntity product, long cost, ref List<string> str, ref long totalFunds)
    {
        var result = Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);

        str.Add(product.company.Name + " Can Maintain " + Format.MinifyMoney(cost) + $" ? {result}");

        return result;
    }

    void TryAddTask(GameEntity product, TeamTask teamTask, ref List<string> str, ref long totalFunds)
    {
        // TODO
        // CHECK TASK SELF COST!!!

        str.Add($"Trying to add task {teamTask.ToString()} to {product.company.Name}");

        if (!CanMaintain(product, Economy.GetTeamTaskCost(product, gameContext, teamTask), ref str, ref totalFunds))
            return;

        int teamId = 0;
        foreach (var t in product.team.Teams)
        {
            if (SupportsTeamTask(t.TeamType, teamTask) && t.Tasks.Count < C.TASKS_PER_TEAM)
            {
                Teams.AddTeamTask(product, gameContext, teamId, teamTask);

                return;
            }

            teamId++;
        }


        // need to hire new team
        // if has money

        var teamCost = Economy.GetSingleTeamCost();
        if (CanMaintain(product, teamCost, ref str, ref totalFunds))
        {
            Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
            Teams.AddTeamTask(product, gameContext, product.team.Teams.Count - 1, teamTask);
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

    void ManageChannels(GameEntity product, ref List<string> str, ref long totalFunds)
    {
        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, true);

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

}
