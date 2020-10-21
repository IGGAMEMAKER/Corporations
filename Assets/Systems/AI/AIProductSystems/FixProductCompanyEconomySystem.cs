using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class FixProductCompanyEconomySystem : OnPeriodChange
{
    public FixProductCompanyEconomySystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        List<string> str = new List<string>();

        var nonFlagshipProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isFlagship).Where(willBeBankrupt);

        foreach (var product in nonFlagshipProducts)
        {
            // Fix situation
            FixEconomy(product, ref str, false);
            // ---------------

            bool isTestCompany = false;

            if (isTestCompany)
            {
                foreach (var s in str)
                    Debug.Log(s);
            }
        }
    }


    void FixEconomy(GameEntity product, ref List<string> str, bool isTestCompany)
    {
        PrintFinancialStatusOfCompany(product, ref str, isTestCompany);

        str.Add(Visuals.Negative($"Economic situation is <b>TERRIBLE</b> in {product.company.Name} (#{product.creationIndex})"));

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
        //PrintFinancialStatusOfCompany(product, ref str, isTestCompany);

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

        PrintFinancialStatusOfCompany(product, ref str, isTestCompany);
        str.Add(Economy.GetProductCompanyMaintenance(product, gameContext, true).MinifyValues().Minify().ToString(true));
    }

    void PrintFinancialStatusOfCompany(GameEntity product, ref List<string> str, bool isTestCompany)
    {
        if (isTestCompany)
        {
            var balance = Economy.BalanceOf(product);

            var income = Economy.GetIncome(gameContext, product);
            var maintenance = Economy.GetMaintenance(gameContext, product);

            var managerMaintenance = Economy.GetManagersCost(product, gameContext);

            var profit = Economy.GetProfit(gameContext, product);

            str.Add(
                $"Balance: " + Format.Money(balance) + ", Profit: " + Visuals.PositiveOrNegativeMinified(profit) +
                " (Income: " + Visuals.Positive("+" + Format.Money(income)) + " Expenses: " + Visuals.Negative("-" + Format.Money(maintenance)) + ")"
                );
        }
    }

    List<TeamTaskDetailed> GetPaidCompanyTeamTasks(GameEntity product) => GetCompanyTeamTasks(product).Where(t => Economy.GetTeamTaskCost(product, gameContext, t.TeamTask) > 0).ToList();

    void RaiseFastCash(GameEntity product)
    {
        //if (Economy.IsCanTakeFastCash(gameContext, product))
        //    Economy.RaiseFastCash(gameContext, product);
    }

    bool willBeBankrupt(GameEntity product) => Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, product);

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

    struct TeamTaskDetailed
    {
        public TeamTask TeamTask;
        public int teamId;
        public int slotId;
    }

    List<TeamTaskDetailed> GetCompanyTeamTasks(GameEntity product)
    {
        var teamTasks = new List<TeamTaskDetailed>();

        var teamId = 0;
        foreach (var team in product.team.Teams)
        {
            for (var i = 0; i < team.Tasks.Count; i++)
                teamTasks.Add(new TeamTaskDetailed { TeamTask = team.Tasks[i], slotId = i, teamId = teamId });

            teamId++;
        }


        return teamTasks;
    }
}
