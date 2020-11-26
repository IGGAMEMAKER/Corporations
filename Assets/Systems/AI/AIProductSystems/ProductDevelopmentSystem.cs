using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ProductActions
{
    Features,
    GrabUsers,
    HandleTeam, // hire more people, add more teams
    //HandleTeam,

    ReleaseApp,

    Monetise,

    GrabSegments,

    ShowProfit,
    RestoreLoyalty,
}

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var nonFlagshipProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isFlagship);

        foreach (var product in nonFlagshipProducts)
        {
            // add goal if there are no goals
            PickNewGoalIfThereAreNoGoals(product);

            foreach (var goal in product.companyGoal.Goals)
            {
                WorkOnGoal(product, goal);
            }

            Investments.CompleteGoals(product, gameContext);
            PickNewGoalIfThereAreNoGoals(product);
        }
    }

    void WorkOnGoal(GameEntity product, InvestmentGoal goal)
    {
        List<ProductActions> actions = new List<ProductActions>();

        //Companies.Log(product, $"Working on goal: {goal.GetFormattedName()}");

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
                actions.Add(ProductActions.GrabUsers);
                break;

            //case InvestorGoalType.ProductPrepareForRelease:
            //    actions.Add(ProductActions.HandleTeam);
            //    actions.Add(ProductActions.ReleaseApp);
            //    break;

            case InvestorGoalType.ProductRelease:
                actions.Add(ProductActions.HandleTeam);
                actions.Add(ProductActions.ReleaseApp);

                break;

            case InvestorGoalType.ProductStartMonetising:
                actions.Add(ProductActions.Monetise);
                actions.Add(ProductActions.Features);

                actions.Add(ProductActions.GrabUsers);
                actions.Add(ProductActions.HandleTeam);
                break;

            case InvestorGoalType.ProductRegainLoyalty:
                actions.Add(ProductActions.Features);
                actions.Add(ProductActions.RestoreLoyalty);

                actions.Add(ProductActions.HandleTeam);
                break;

            case InvestorGoalType.GainMoreSegments:
                actions.Add(ProductActions.RestoreLoyalty);
                actions.Add(ProductActions.GrabSegments);
                actions.Add(ProductActions.Features);
                actions.Add(ProductActions.GrabUsers);

                actions.Add(ProductActions.HandleTeam);
                break;

            case InvestorGoalType.GrowUserBase:
                actions.Add(ProductActions.GrabUsers);
                actions.Add(ProductActions.Features);

                actions.Add(ProductActions.HandleTeam);
                break;

            case InvestorGoalType.GrowIncome:
                actions.Add(ProductActions.Features);
                actions.Add(ProductActions.GrabUsers);

                actions.Add(ProductActions.HandleTeam);
                break;

            case InvestorGoalType.BecomeProfitable:
                actions.Add(ProductActions.GrabUsers);
                actions.Add(ProductActions.Features);
                actions.Add(ProductActions.HandleTeam);

                break;

            default:
                Companies.Log(product, "DEFAULT goal");

                actions.Add(ProductActions.GrabUsers);
                actions.Add(ProductActions.Features);

                actions.Add(ProductActions.HandleTeam);
                break;
        }

        foreach (var action in actions)
        {
            ManageProduct(action, product);
        }

        //Investments.CompleteGoal(product, gameContext, goal);
    }

    private void ManageProduct(ProductActions action, GameEntity product)
    {
        //Companies.Log(product, "Actions." + action.ToString());
        switch (action)
        {
            case ProductActions.Features:
                ManageFeatures(product);
                break;

            case ProductActions.ReleaseApp:
                ReleaseApp(product);
                break;

            case ProductActions.Monetise:
                Monetise(product);
                break;

            case ProductActions.GrabUsers:
                ManageChannels(product);
                break;

            case ProductActions.GrabSegments:
                GrabSegments(product);
                break;

            case ProductActions.HandleTeam:
                ExpandTeam(product);
                break;

            case ProductActions.RestoreLoyalty:
                DeMonetise(product);
                break;

            default:
                var lg = Visuals.Negative("UNKNOWN ACTION in ProductDevelopmentSystem: " + action);
                Companies.Log(product, lg);

                Debug.LogError(lg);

                break;
        }
    }

    void PickNewGoalIfThereAreNoGoals(GameEntity product)
    {
        if (product.companyGoal.Goals.Count == 0)
        {
            var pickableGoals = Investments.GetNewGoals(product, gameContext);

            var max = pickableGoals.Count;

            if (max == 0)
            {
                Companies.LogFail(product, "CANNOT GET A GOAL");

                Debug.LogError("CANNOT GET A GOAL FOR: " + product.company.Name + "\n\nCompleted goals\n\n" + string.Join(", ", product.completedGoals.Goals));
            }
            else
            {
                //Companies.Log(product, $"Choosing between {max} goals: {string.Join(", ", pickableGoals.Select(g => g.GetFormattedName()))}");

                Investments.AddCompanyGoal(product, gameContext, pickableGoals[UnityEngine.Random.Range(0, max)]);
            }
        }
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

        if (!Teams.HasFreeSlotForTeamTask(product, teamTask))
        {
            // need to hire new team
            var teamCost = Economy.GetSingleTeamCost();

            if (CanMaintain(product, teamCost + taskCost))
            {
                if (teamTask.IsHighloadTask)
                {
                    TryAddTeam(product, TeamType.ServersideTeam);
                    //Teams.AddTeam(product, gameContext, TeamType.ServersideTeam);
                }
                else
                {
                    TryAddTeam(product, TeamType.CrossfunctionalTeam);
                    //Teams.AddTeam(product, gameContext, TeamType.CrossfunctionalTeam);
                }

                teamId = product.team.Teams.Count - 1;
            }
        }

        if (Teams.HasFreeSlotForTeamTask(product, teamTask))
        {
            Teams.AddTeamTask(product, gameContext, teamId, teamTask);
        }
    }
}
