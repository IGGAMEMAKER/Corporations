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
            {
                ManageProduct(product);
            }
        }
    }

    void PrintFinancialStatusOfCompany(GameEntity product, ref List<string> str)
    {
        var balance = Economy.BalanceOf(product);

        var income = Economy.GetCompanyIncome(gameContext, product);
        var maintenance = Economy.GetCompanyMaintenance(gameContext, product);

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);

        var profit = Economy.GetProfit(gameContext, product);

        str.Add(
            $"Balance: " + Format.Money(balance) + ", Profit: " + Visuals.PositiveOrNegativeMinified(profit) + 
            " (Income: " + Visuals.Positive("+" + Format.Money(income)) + " Expenses: " + Visuals.Negative("-" + Format.Money(maintenance)) + ")"
            );
    }

    void ManageProduct(GameEntity product)
    {
        List<string> str = new List<string>();

        Economy.RaiseFastCash(gameContext, product);

        if (Companies.IsReleaseableApp(product, gameContext))
        {
            Marketing.ReleaseApp(gameContext, product);
        }

        str.Add($"------------------ {product.company.Name} (#{product.creationIndex}) -------------------");
        PrintFinancialStatusOfCompany(product, ref str);
        //str.Add("Money available: " + Visuals.PositiveOrNegativeMinified(totalFunds));

        // features (free)
        // servers and support
        // marketing channels (growth)

        ManageFeatures(product, ref str);
        ManageSupport(product, ref str);
        ManageChannels(product, ref str);

        PrintFinancialStatusOfCompany(product, ref str);
        str.Add(Economy.GetProductCompanyMaintenance(product, gameContext, true).MinifyValues().Minify().ToString(true));

        if (willBeBankrupt(product))
        {
            FixEconomy(product, ref str);
        }

        bool isTestCompany = product.company.Name == "Money Exchange 0"; // IsInPlayerSphereOfInterest(product)
        if (isTestCompany)
        {
            foreach (var s in str)
                Debug.Log(s);
        }
    }

    bool willBeBankrupt(GameEntity product) => Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, product);

    void RaiseFastCash(GameEntity product)
    {
        if (Economy.IsCanTakeFastCash(gameContext, product))
            Economy.RaiseFastCash(gameContext, product);
    }

    void RemoveExpensiveTask(GameEntity product, ref List<string> str)
    {
        var tasks = GetPaidCompanyTeamTasks(product)
            .OrderByDescending(t => Economy.GetTeamTaskCost(product, gameContext, t.TeamTask))
            .ToList();

        var expensiveTask = tasks.First();

        var cost = Economy.GetTeamTaskCost(product, gameContext, expensiveTask.TeamTask);
        str.Add("Found expensive task: " + expensiveTask.TeamTask.GetTaskName() + $"teamId = {expensiveTask.teamId} taskId = {expensiveTask.slotId}");

        Teams.RemoveTeamTask(product, gameContext, expensiveTask.teamId, expensiveTask.slotId);
        Debug.Log("Removed expensive task " + expensiveTask.TeamTask.GetTaskName());
    }

    string GetPaidCompanyTasksDescription(GameEntity product)
    {
        var paidTasks = GetPaidCompanyTeamTasks(product);
        var compactTasks = string.Join("\n", paidTasks.Select(t => t.TeamTask.GetTaskName() + $" in team{t.teamId} [{t.slotId}]"));

        return $"Paid tasks: <{paidTasks.Count}> \n" + compactTasks;
    }

    void RenderPaidTasks(GameEntity product, ref List<string> str)
    {
        var tasks = GetPaidCompanyTeamTasks(product);
        str.Add($"Paid tasks: <{tasks.Count}> \n" + GetPaidCompanyTasksDescription(product));
    }

    void FixEconomy(GameEntity product, ref List<string> str)
    {
        str.Add(Visuals.Negative("Economic situation is <b>TERRIBLE</b> in " + product.company.Name));

        // take investments
        RaiseFastCash(product);
        RaiseFastCash(product);
        RaiseFastCash(product);


        if (!willBeBankrupt(product))
        {
            str.Add(Visuals.Positive("CASH SAVED US!"));
            return;
        }

        str.Add(Visuals.Negative("Took cash, but this didn't help much"));
        PrintFinancialStatusOfCompany(product, ref str);

        var paidTasks = GetPaidCompanyTeamTasks(product);

        RenderPaidTasks(product, ref str);

        int triesMax = paidTasks.Count;
        int tries = 0;

        while (willBeBankrupt(product) && tries < triesMax)
        {
            tries++;
            if (GetPaidCompanyTeamTasks(product).Count > 0)
            {
                RemoveExpensiveTask(product, ref str);
            }
            else
            {
                str.Add("No more tasks left");
                break;
            }
        }

        RenderPaidTasks(product, ref str);

        if (!willBeBankrupt(product))
        {
            str.Add(Visuals.Positive("Removing expensive tasks helped!"));
            return;
        }

        str.Add("<b>BANKRUPTCY IS HERE...</b>");

        // remove teams without tasks
        
    }

    struct TeamTaskDetailed
    {
        public TeamTask TeamTask;
        public int teamId;
        public int slotId;
    }


    List<TeamTaskDetailed> GetPaidCompanyTeamTasks(GameEntity product) => GetCompanyTeamTasks(product).Where(t => Economy.GetTeamTaskCost(product, gameContext, t.TeamTask) > 0).ToList();

    List<TeamTaskDetailed> GetCompanyTeamTasks(GameEntity product)
    {
        var teamTasks = new List<TeamTaskDetailed>();

        var teamId = 0;
        foreach (var team in product.team.Teams)
        {
            for (var i = 0; i < team.Tasks.Count; i++)
                teamTasks.Add(new TeamTaskDetailed { TeamTask = team.Tasks[i], slotId = i, teamId = teamId });
        }

        teamId++;

        return teamTasks;
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
        var channels = Markets.GetAvailableMarketingChannels(gameContext, product, false)
            //.Where(c => !Marketing.IsCompanyActiveInChannel(product, c))
            .Where(c => Marketing.IsChannelProfitable(product, gameContext, c))
            .OrderBy(c => Marketing.GetChannelRepaymentSpeed(product, gameContext, c));

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
        if (!CanMaintain(product, taskCost, ref str))
        {
            str.Add("-- " + Visuals.Negative("This task is too expensive!") + $" Need {Format.MinifyMoney(taskCost)}  --");

            return;
        }

        int teamId = 0;
        foreach (var t in product.team.Teams)
        {
            var hasFreeSlot = t.Tasks.Count < C.TASKS_PER_TEAM;
            if (SupportsTeamTask(t.TeamType, teamTask) && hasFreeSlot)
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
}
