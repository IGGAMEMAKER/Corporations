using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
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

    public static partial class Investments
    {
        public static List<ProductActions> GetProductActions(GameEntity product, InvestmentGoal goal)
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

            return actions;
        }
    }
}
