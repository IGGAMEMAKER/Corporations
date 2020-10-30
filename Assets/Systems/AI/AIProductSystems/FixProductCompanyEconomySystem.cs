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
            FixEconomy(product);
        }
    }


    void FixEconomy(GameEntity product)
    {
        PrintFinancialStatusOfCompany(product);

        Companies.Log(product, Visuals.Negative($"Economic situation is <b>TERRIBLE</b> in {product.company.Name} (#{product.creationIndex})"));

        // take investments
        RaiseFastCash(product);
        RaiseFastCash(product);
        RaiseFastCash(product);


        if (!willBeBankrupt(product))
        {
            Companies.Log(product, Visuals.Positive("CASH SAVED US!"));
            return;
        }

        Companies.Log(product, Visuals.Negative("Took cash, but this didn't help much"));
        //PrintFinancialStatusOfCompany(product, ref str, isTestCompany);

        var paidTasks = GetPaidCompanyTeamTasks(product);

        RenderPaidTasks(product);

        int triesMax = paidTasks.Count;
        int tries = 0;

        

        while (willBeBankrupt(product) && tries < triesMax)
        {
            tries++;
            if (GetPaidCompanyTeamTasks(product).Count > 0)
            {
                RemoveExpensiveTask(product);
            }
            else
            {
                Companies.Log(product, "No more tasks left");
                break;
            }
        }

        RenderPaidTasks(product);

        if (!willBeBankrupt(product))
        {
            Companies.Log(product, Visuals.Positive("Removing expensive tasks helped!"));
            return;
        }

        Companies.Log(product, "<b>BANKRUPTCY IS HERE...</b>");

        // remove teams without tasks

        PrintFinancialStatusOfCompany(product);
        Companies.Log(product, Economy.GetProductCompanyMaintenance(product, true).MinifyValues().Minify().ToString(true));
    }

    void PrintFinancialStatusOfCompany(GameEntity product)
    {
        var balance = Economy.BalanceOf(product);

        var income = Economy.GetIncome(gameContext, product);
        var maintenance = Economy.GetMaintenance(gameContext, product);

        var managerMaintenance = Economy.GetManagersCost(product);

        var profit = Economy.GetProfit(gameContext, product);

        Companies.Log(product,
            $"Balance: " + Format.Money(balance) + ", Profit: " + Visuals.PositiveOrNegativeMinified(profit) +
            " (Income: " + Visuals.Positive("+" + Format.Money(income)) + " Expenses: " + Visuals.Negative("-" + Format.Money(maintenance)) + ")"
            );
    }

    List<TeamTaskDetailed> GetPaidCompanyTeamTasks(GameEntity product) => GetCompanyTeamTasks(product).Where(t => Economy.GetTeamTaskCost(product, t.TeamTask) > 0).ToList();

    void RaiseFastCash(GameEntity product)
    {
        //if (Economy.IsCanTakeFastCash(gameContext, product))
        //    Economy.RaiseFastCash(gameContext, product);
    }

    bool willBeBankrupt(GameEntity product) => Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, product);

    void RemoveExpensiveTask(GameEntity product)
    {
        var tasks = GetPaidCompanyTeamTasks(product)
            .OrderByDescending(t => Economy.GetTeamTaskCost(product, t.TeamTask))
            .ToList();

        var expensiveTask = tasks.First();

        var cost = Economy.GetTeamTaskCost(product, expensiveTask.TeamTask);
        Companies.Log(product, "Found expensive task: " + expensiveTask.TeamTask.GetTaskName() + $"teamId = {expensiveTask.teamId} taskId = {expensiveTask.slotId}");

        Teams.RemoveTeamTask(product, gameContext, expensiveTask.teamId, expensiveTask.slotId);
        Companies.Log(product, "Removed task " + expensiveTask.TeamTask.GetTaskName());
    }

    string GetPaidCompanyTasksDescription(GameEntity product)
    {
        var paidTasks = GetPaidCompanyTeamTasks(product);
        var compactTasks = string.Join("\n", paidTasks.Select(t => t.TeamTask.GetTaskName() + $" in team{t.teamId} [{t.slotId}]"));

        return $"Paid tasks: <{paidTasks.Count}> \n" + compactTasks;
    }

    void RenderPaidTasks(GameEntity product)
    {
        var tasks = GetPaidCompanyTeamTasks(product);

        Companies.Log(product, $"Paid tasks: <{tasks.Count}> \n" + GetPaidCompanyTasksDescription(product));
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
