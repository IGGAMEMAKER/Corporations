using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {

        public static List<InvestmentGoal> GetProductGoals(GameEntity product, GameContext Q)
        {
            var goals = new List<InvestmentGoal>();

            // productOnly goals
            var productGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductPrototype,
                InvestorGoalType.ProductFirstUsers,
                InvestorGoalType.ProductBecomeMarketFit,
                InvestorGoalType.ProductRelease,

                InvestorGoalType.ProductStartMonetising,
                InvestorGoalType.GrowUserBase,
                InvestorGoalType.OutcompeteCompanyByUsers,
                InvestorGoalType.GainMoreSegments,

                InvestorGoalType.ProductRegainLoyalty,
            };

            #region data
            bool focusedProduct = Marketing.IsFocusingOneAudience(product);
            var marketFit = 10; // 10 cause it allows monetisation for ads


            bool isPrototype = !product.isRelease && focusedProduct;
            bool releasedProduct = product.isRelease;

            long users = Marketing.GetUsers(product);

            var amountOfAudiences = Marketing.GetAudienceInfos().Count;
            var ourAudiences = Marketing.GetAmountOfTargetAudiences(product);
            #endregion

            if (isPrototype)
            {
                bool madePrototype = Completed(product, InvestorGoalType.ProductPrototype);
                bool isMarketFit = Completed(product, InvestorGoalType.ProductBecomeMarketFit);
                bool gotFirstUsers = Completed(product, InvestorGoalType.ProductFirstUsers);

                var coreLoyalty = Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(product));

                // has no goals at start
                if (!Completed(product, InvestorGoalType.ProductPrototype))
                    //return OnlyGoal(InvestorGoalType.ProductPrototype);
                    return OnlyGoal(new InvestmentGoalMakePrototype());

                if (Completed(product, InvestorGoalType.ProductPrototype))
                    //AddOnce(goals, product, InvestorGoalType.ProductFirstUsers);
                    AddOnce(goals, product, new InvestmentGoalFirstUsers(5_000));

                if (Completed(product, InvestorGoalType.ProductFirstUsers))
                    //AddOnce(goals, product, InvestorGoalType.ProductBecomeMarketFit);
                    AddOnce(goals, product, new InvestmentGoalMakeProductMarketFit());

                if (Completed(product, InvestorGoalType.ProductBecomeMarketFit))
                    //AddOnce(goals, product, InvestorGoalType.ProductPrepareForRelease);
                    AddOnce(goals, product, new InvestmentGoalPrepareForRelease());

                if (Completed(product, InvestorGoalType.ProductPrepareForRelease))
                    //AddOnce(goals, product, InvestorGoalType.ProductRelease);
                    AddOnce(goals, product, new InvestmentGoalRelease());
            }

            if (releasedProduct)
            {
                if (Completed(product, InvestorGoalType.ProductRelease))
                    //AddOnce(goals, product, InvestorGoalType.ProductStartMonetising);
                    AddOnce(goals, product, new InvestmentGoalStartMonetisation());

                if (Completed(product, InvestorGoalType.ProductStartMonetising))
                    //goals.Add(InvestorGoalType.GrowUserBase);
                    goals.Add(new InvestmentGoalGrowAudience(Marketing.GetUsers(product) * 2));

                if (Completed(product, InvestorGoalType.GrowUserBase))
                {
                    bool canGetMoreAudiences = ourAudiences < amountOfAudiences;
                    bool needsToExpand = Marketing.GetAudienceInfos()
                        .Where(s => Marketing.IsAimingForSpecificAudience(product, s.ID))
                        .All(s => Marketing.GetUsers(product, s.ID) > 1_000_000);

                    if (users < 1_000_000)
                    {
                        //AddOnce(goals, product, InvestorGoalType.ProductMillionUsers);
                        AddOnce(goals, product, new InvestmentGoalMillionUsers(1_000_000));
                    }

                    if (users >= 1_000_000 && canGetMoreAudiences && needsToExpand)
                    {
                        //goals.Add(InvestorGoalType.GainMoreSegments);
                        goals.Add(new InvestmentGoalMoreSegments(ourAudiences + 1));
                    }

                    if (users > 50_000_000 && canGetMoreAudiences)
                    {
                        // globalise
                        //return OnlyGoal(InvestorGoalType.GainMoreSegments);
                        return OnlyGoal(new InvestmentGoalMoreSegments(ourAudiences + 1));
                    }
                }
            }

            if (Marketing.IsHasDisloyalAudiences(product))
                //return OnlyGoal(InvestorGoalType.ProductRegainLoyalty);
                return OnlyGoal(new InvestmentGoalRegainLoyalty());

            return goals;
        }

    }
}
