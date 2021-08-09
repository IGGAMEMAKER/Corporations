using System.Collections.Generic;
using System.Linq;

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
            bool isPrototype = !product.isRelease;
            bool releasedProduct = product.isRelease;

            long users = Marketing.GetUsers(product);
            #endregion

            if (isPrototype)
            {
                // has no goals at start
                if (!Completed(product, InvestorGoalType.ProductPrototype))
                    return OnlyGoal(new InvestmentGoalMakePrototype());
                else
                    AddOnce(goals, product, new InvestmentGoalFirstUsers(5_000));

                if (Completed(product, InvestorGoalType.ProductFirstUsers))
                    AddOnce(goals, product, new InvestmentGoalMakeProductMarketFit());

                if (Completed(product, InvestorGoalType.ProductBecomeMarketFit))
                    AddOnce(goals, product, new InvestmentGoalPrepareForRelease());

                if (Completed(product, InvestorGoalType.ProductPrepareForRelease))
                    AddOnce(goals, product, new InvestmentGoalRelease());
            }

            if (releasedProduct)
            {
                if (Completed(product, InvestorGoalType.ProductRelease))
                    AddOnce(goals, product, new InvestmentGoalStartMonetisation());

                if (Completed(product, InvestorGoalType.ProductStartMonetising))
                    goals.Add(new InvestmentGoalGrowAudience(Marketing.GetUsers(product) * 2));

                if (Completed(product, InvestorGoalType.GrowUserBase))
                {
                    if (users < 1_000_000)
                    {
                        AddOnce(goals, product, new InvestmentGoalMillionUsers(1_000_000));
                        goals.RemoveAll(g => g.InvestorGoalType == InvestorGoalType.GrowUserBase);
                    }

                    // protect from no goals situation
                    if (goals.Count == 0)
                    {
                        goals.Add(new InvestmentGoalGrowAudience(Marketing.GetUsers(product) * 2));
                    }
                }
            }

            /*if (Marketing.IsHasDisloyalAudiences(product))
                return OnlyGoal(new InvestmentGoalRegainLoyalty());*/
                //return OnlyGoal(InvestorGoalType.ProductRegainLoyalty);

            return goals;
        }

    }
}
