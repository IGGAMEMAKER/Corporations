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

    ReleaseApp,

    Monetise,

    ShowProfit,
    RestoreLoyalty,
}

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    public ProductDevelopmentSystem(Contexts contexts) : base(contexts) { }


    protected override void Execute(List<GameEntity> entities)
    {
        var nonFlagshipProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isFlagship);

        Markup($"\n<b>Products: {nonFlagshipProducts.Count()}</b>\n");
        
        var timeX = DateTime.Now;

        ManageProducts(nonFlagshipProducts);
        
        MeasureTag("TOTAL PRODUCT DEVELOPMENT TOOK", timeX);
    }

    void ManageProducts(IEnumerable<GameEntity> nonFlagshipProducts)
    {
        foreach (var product in nonFlagshipProducts)
        {
            var time0 = DateTime.Now;
            
            PickNewGoalIfThereAreNoGoals(product);
            if (product.companyGoal.Goals.Count == 0)
                continue;

            WorkOnGoals(product);

            CompleteGoals(product);
            
            PickNewGoalIfThereAreNoGoals(product);
            
            Measure($"<b>Managing product</b>", product, time0);
            Markup("\n");
        }
    }

    void CompleteGoals(GameEntity product)
    {
        var time = DateTime.Now;
            
        Investments.CompleteGoals(product, gameContext);
        Measure("Complete goals ", product, time);
    }

    void WorkOnGoals(GameEntity product)
    {
        foreach (var goal in product.companyGoal.Goals)
        {
            var time1 = DateTime.Now;
                
            WorkOnGoal(product, goal);
                
            Measure("<i>Work on goal</i> ", product, time1);
        }
    }

    void WorkOnGoal(GameEntity product, InvestmentGoal goal)
    {
        var actions = new List<ProductActions>();

        actions.Add(ProductActions.HandleTeam);

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
        var time = DateTime.Now;
        
        //Companies.Log(product, "Actions." + action.ToString());
        switch (action)
        {
            case ProductActions.Features:
                ManageFeatures(product);
                MeasureTag("* Features", time);

                break;

            case ProductActions.ReleaseApp:
                ReleaseProduct(product);
                MeasureTag("* Release", time);
                
                break;

            case ProductActions.Monetise:
                Monetize(product);
                MeasureTag("* Monetize", time);

                break;

            case ProductActions.GrabUsers:
                ManageChannels(product);
                MeasureTag("* Channels", time);

                break;

            case ProductActions.HandleTeam:
                HandleTeam(product);
                MeasureTag("* Teams", time);

                break;

            case ProductActions.RestoreLoyalty:
                DeMonetize(product);
                MeasureTag("* Restore loyalty", time);

                break;

            default:
                var lg = Visuals.Negative("UNKNOWN ACTION in ProductDevelopmentSystem: " + action);
                Companies.Log(product, lg);

                MeasureTag("* <B>DEFAULT PRODUCT ACTION</B>", time);

                Debug.LogError(lg);

                break;
        }
    }

    void PickNewGoalIfThereAreNoGoals(GameEntity product)
    {
        if (product.companyGoal.Goals.Count == 0)
        {
            var time = DateTime.Now;
            
            var pickableGoals = Investments.GetNewGoals(product, gameContext);

            var max = pickableGoals.Count;

            if (max == 0)
            {
                Companies.LogFail(product, "CANNOT GET A GOAL FOR: " + product.company.Name + "\n\nCompleted goals\n\n" + string.Join(", ", product.completedGoals.Goals));
            }
            else
            {
                //Companies.Log(product, $"Choosing between {max} goals: {string.Join(", ", pickableGoals.Select(g => g.GetFormattedName()))}");

                Investments.AddCompanyGoal(product, gameContext, pickableGoals[UnityEngine.Random.Range(0, max)]);
            }
            
            MeasureTag("Pick Goal ", time);
        }
    }


    // -----------------------------------------------------------------------------------

    bool CanMaintain(GameEntity product, long cost)
    {
        if (cost == 0)
            return true;

        return Economy.IsCanMaintainForAWhile(product, gameContext, cost, 1);
    }

}
