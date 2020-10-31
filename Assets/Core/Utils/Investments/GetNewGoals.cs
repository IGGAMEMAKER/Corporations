using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetNewGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestmentGoal>();

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            // product only
            if (isProduct)
            {
                goals.AddRange(GetWrappedProductGoals(company, Q));
            }

            // group only
            if (isGroup)
            {
                // daughters need to be dependant on parent company
                foreach (var daughter in Companies.GetDaughterProducts(Q, company))
                {
                    goals.AddRange(GetWrappedProductGoals(daughter, Q, company));
                }

                goals.AddRange(GetWrappedGroupGoals(company, Q));
            }

            // both
            goals.AddRange(GetWrappedCommonGoals(company, Q));

            return goals;
        }

        private static List<InvestmentGoal> GetWrappedProductGoals(GameEntity company, GameContext Q, GameEntity controller = null)
        {
            return Wrap(GetProductGoals(company, Q), company, Q, controller);
        }

        private static List<InvestmentGoal> GetWrappedGroupGoals(GameEntity company, GameContext Q)
        {
            return Wrap(GetGroupOnlyGoals(company, Q), company, Q);
        }

        private static List<InvestmentGoal> GetWrappedCommonGoals(GameEntity company, GameContext Q)
        {
            return Wrap(GetCommonGoals(company, Q), company, Q);
        }

        public static bool HasGoalAlready(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType);
        }
        // IsGoalDoneOrOutgrown
        public static bool GoalCanBeRepeated(InvestorGoalType goal)
        {
            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductPrototype,
                InvestorGoalType.ProductFirstUsers,
                InvestorGoalType.ProductBecomeMarketFit,
                InvestorGoalType.ProductRelease,

                InvestorGoalType.ProductStartMonetising,
            };

            return !OneTimeGoals.Contains(goal);
        }

        public static bool Completed(GameEntity company1, InvestorGoalType goal) => IsGoalCompleted(company1, goal);
        public static bool IsGoalCompleted(GameEntity company, InvestorGoalType goal)
        {
            // goal was done or outreached
            bool done = company.completedGoals.Goals.Contains(goal);

            return done;
            //if (done && OneTimeGoals.Contains(goal))
            //    return true;

            //var goal1 = Investments.GetInvestmentGoal(company, gameContext, goal).SetExecutorAndController(company, company);
            //bool outgrown = Investments.CanCompleteGoal(company, gameContext, goal1);

            //if (outgrown && OneTimeGoals.Contains(goal))
            //{
            //    Investments.CompleteGoal(company, gameContext, goal1, true);
            //    return true;
            //}

            //return false;
        }

        public static void AddOnce(List<InvestorGoalType> goals, GameEntity product, InvestorGoalType goal)
        {
            if (!Completed(product, goal))
                goals.Add(goal);
        }

        public static List<InvestorGoalType> GetProductGoals(GameEntity product, GameContext Q)
        {
            var goals = new List<InvestorGoalType>();

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
                    return OnlyGoal(InvestorGoalType.ProductPrototype);

                if (Completed(product, InvestorGoalType.ProductPrototype))
                    AddOnce(goals, product, InvestorGoalType.ProductFirstUsers);

                if (Completed(product, InvestorGoalType.ProductFirstUsers) && coreLoyalty < marketFit)
                    AddOnce(goals, product, InvestorGoalType.ProductBecomeMarketFit);

                if (Completed(product, InvestorGoalType.ProductBecomeMarketFit))
                    AddOnce(goals, product, InvestorGoalType.ProductRelease);
            }

            if (releasedProduct)
            {
                if (Completed(product, InvestorGoalType.ProductRelease))
                    AddOnce(goals, product, InvestorGoalType.ProductStartMonetising);

                if (Completed(product, InvestorGoalType.ProductStartMonetising))
                    goals.Add(InvestorGoalType.GrowUserBase);

                if (users > 500_000 && ourAudiences < amountOfAudiences)
                    goals.Add(InvestorGoalType.GainMoreSegments);
            }

            if (Marketing.IsHasDisloyalAudiences(product))
                return OnlyGoal(InvestorGoalType.ProductRegainLoyalty);

            return goals;
        }

        public static List<InvestorGoalType> GetGroupOnlyGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestorGoalType>();

            var groupGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.AcquireCompany,
                InvestorGoalType.IPO,
            };

            #region data
            var income = Economy.GetIncome(Q, company);

            bool solidCompany = income > 500_000;

            var weakerCompany = Companies.GetWeakerCompetitor(company, Q);
            bool hasWeakerCompanies = weakerCompany != null;
            #endregion

            if (solidCompany && hasWeakerCompanies)
                goals.Add(InvestorGoalType.AcquireCompany);

            return goals;
        }

        public static List<InvestorGoalType> GetCommonGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestorGoalType>();

            var both = new List<InvestorGoalType>
            {
                InvestorGoalType.OutcompeteCompanyByCost,
                InvestorGoalType.OutcompeteCompanyByIncome,

                InvestorGoalType.GrowCompanyCost,
                InvestorGoalType.GrowIncome,

                InvestorGoalType.BecomeProfitable
            };

            #region data
            bool releasedProduct = company.hasProduct && company.isRelease;

            bool isGroup = !company.hasProduct;

            var income = Economy.GetIncome(Q, company);
            bool profitable = Economy.IsProfitable(Q, company);

            bool solidCompany = (releasedProduct || isGroup) && income > 500_000;

            bool hasStrongerCompanies = Companies.GetStrongerCompetitor(company, Q) != null;
            #endregion

            if (solidCompany)
                goals.Add(InvestorGoalType.GrowCompanyCost);

            if (solidCompany && hasStrongerCompanies)
            {
                goals.Add(InvestorGoalType.OutcompeteCompanyByIncome);
                goals.Add(InvestorGoalType.OutcompeteCompanyByCost);
                //goals.Add(InvestorGoalType.OutcompeteCompanyByUsers);
            }

            if (solidCompany && !profitable)
                goals.Add(InvestorGoalType.BecomeProfitable);

            return goals;
        }


        public static List<InvestorGoalType> OnlyGoal(InvestorGoalType goal) => new List<InvestorGoalType> { goal };
        public static List<InvestmentGoal> Wrap(List<InvestorGoalType> goals, GameEntity executor, GameContext Q, GameEntity controller = null)
        {
            return goals
                .Where(g => !HasGoalAlready(executor, Q, g))
                
                .Select(g => GetInvestmentGoal(executor, Q, g).SetExecutorAndController(executor, controller ?? executor))
                .ToList();
        }
    }
}
