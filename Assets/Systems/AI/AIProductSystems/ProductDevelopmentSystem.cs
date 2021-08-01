﻿using Assets.Core;
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
        if (product.companyGoal.Goals.Any())
            return;

        var time = DateTime.Now;

        var pickableGoals = Investments.GetNewGoals(product, gameContext);

        if (pickableGoals.Any())
        {
            Investments.AddCompanyGoal(product, gameContext, RandomUtils.RandomItem(pickableGoals));
        }
        else
        {
            Companies.LogFail(product, "CANNOT GET A GOAL FOR: " + product.company.Name + "\n\nCompleted goals\n\n" + string.Join(", ", product.completedGoals.Goals));
        }

        MeasureTag("Pick Goal ", time);
    }
}
