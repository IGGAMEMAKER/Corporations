using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem
{
    void WorkOnGoal(GameEntity product, InvestmentGoal goal)
    {
        var actions = Investments.GetProductActions(product, goal);

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
