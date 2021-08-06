using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem
{
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

            case InvestorGoalType.ProductPrepareForRelease:
                actions.Add(ProductActions.HandleTeam);
                actions.Add(ProductActions.ReleaseApp);
                break;

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
}
