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
        str.Add("Income: " + Format.Money(income));

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);
        var totalFunds = balance + income - managerMaintenance;
        str.Add("Money available for upgrades: " + Format.Money(totalFunds));

        // features (free)
        // servers and support
        // marketing channels (growth)

        ManageFeatures(product);
        ManageSupport(product);
        ManageChannels(product, ref str, ref totalFunds);
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

    bool CanMaintain(GameEntity product, long cost)
    {
        return Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);
    }

    void TryAddTask(GameEntity product, TeamTask teamTask)
    {
        int teamId = 0;

        // TODO
        // CHECK TASK SELF COST!!!

        if (!CanMaintain(product, Economy.GetTeamTaskCost(product, gameContext, teamTask)))
            return;

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

        var teamCost = 8 * C.SALARIES_PROGRAMMER;
        if (CanMaintain(product, teamCost))
        {
            Teams.AddTeam(product, TeamType.CrossfunctionalTeam);
            Teams.AddTeamTask(product, gameContext, product.team.Teams.Count - 1, teamTask);
        }
    }

    void ManageFeatures(GameEntity product)
    {
        var remainingFeatures = Products.GetAvailableFeaturesForProduct(product).Where(f => !Products.IsUpgradingFeature(product, gameContext, f.Name));

        if (remainingFeatures.Count() == 0)
            return;

        var feature = new TeamTaskFeatureUpgrade(remainingFeatures.First());

        TryAddTask(product, feature);
    }

    void ManageSupport(GameEntity product)
    {
        if (Products.IsNeedsMoreServers(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetServerCapacity(product);

            var supportFeatures = Products.GetHighloadFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature));
        }

        if (Products.IsNeedsMoreMarketingSupport(product))
        {
            var diff = Products.GetClientLoad(product) - Products.GetSupportCapacity(product);

            var supportFeatures = Products.GetMarketingSupportFeatures(product);
            var feature = supportFeatures[2];

            TryAddTask(product, new TeamTaskSupportFeature(feature));
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

            TryAddTask(product, new TeamTaskChannelActivity(c.marketingChannel.ChannelInfo.ID));
            //totalFunds = CheckChannelCosts(product, c, totalFunds, ref str);
            //if (Marketing.IsCompanyActiveInChannel())
        }
    }

}
