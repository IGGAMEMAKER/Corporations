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
                    //return OnlyGoal(InvestorGoalType.ProductPrototype);

                if (Completed(product, InvestorGoalType.ProductPrototype))
                    AddOnce(goals, product, new InvestmentGoalFirstUsers(5_000));
                    //AddOnce(goals, product, InvestorGoalType.ProductFirstUsers);

                if (Completed(product, InvestorGoalType.ProductFirstUsers))
                    AddOnce(goals, product, new InvestmentGoalMakeProductMarketFit());
                    //AddOnce(goals, product, InvestorGoalType.ProductBecomeMarketFit);

                if (Completed(product, InvestorGoalType.ProductBecomeMarketFit))
                    AddOnce(goals, product, new InvestmentGoalPrepareForRelease());
                    //AddOnce(goals, product, InvestorGoalType.ProductPrepareForRelease);

                if (Completed(product, InvestorGoalType.ProductPrepareForRelease))
                    AddOnce(goals, product, new InvestmentGoalRelease());
                    //AddOnce(goals, product, InvestorGoalType.ProductRelease);
            }

            if (releasedProduct)
            {
                if (Completed(product, InvestorGoalType.ProductRelease))
                    AddOnce(goals, product, new InvestmentGoalStartMonetisation());
                    //AddOnce(goals, product, InvestorGoalType.ProductStartMonetising);

                if (Completed(product, InvestorGoalType.ProductStartMonetising))
                    goals.Add(new InvestmentGoalGrowAudience(Marketing.GetUsers(product) * 2));
                    //goals.Add(InvestorGoalType.GrowUserBase);

                if (Completed(product, InvestorGoalType.GrowUserBase))
                {
                    if (users < 1_000_000)
                    {
                        //AddOnce(goals, product, InvestorGoalType.ProductMillionUsers);
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
