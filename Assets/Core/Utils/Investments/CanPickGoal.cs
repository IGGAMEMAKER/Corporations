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
                goals.AddRange(GetProductGoals(company, Q, company));
            }

            // group only
            if (isGroup)
            {
                // daughters need to be dependant on parent company
                foreach (var d in Companies.GetDaughterProducts(Q, company))
                    goals.AddRange(GetProductGoals(d, Q, company));

                goals.AddRange(GetGroupOnlyGoals(company, Q, company));
            }

            // both
            goals.AddRange(GetCommonGoals(company, Q, company));

            return goals;
        }

        public static bool HasGoalAlready(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType);
        }
        // IsGoalDoneOrOutgrown
        public static bool Completed(GameEntity company1, InvestorGoalType goal, GameContext gameContext) => IsGoalDoneOrOutgrown(company1, goal, gameContext);
        public static bool IsGoalDoneOrOutgrown(GameEntity company, InvestorGoalType goal, GameContext gameContext)
        {
            //var company = GetGoalPickingCompany(company1, gameContext, goal);

            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductRelease,
                InvestorGoalType.ProductBecomeMarketFit,
                InvestorGoalType.ProductFirstUsers,
                InvestorGoalType.ProductPrototype,
            };

            // goal was done or outreached
            bool done = company.completedGoals.Goals.Contains(goal);

            if (done && OneTimeGoals.Contains(goal))
                return true;

            var goal1 = Investments.GetInvestmentGoal(company, gameContext, goal);
            bool outgrown = Investments.CanCompleteGoal(company, gameContext, goal1);

            if (outgrown && OneTimeGoals.Contains(goal))
            {
                Investments.CompleteGoal(company, gameContext, goal1, true);
                return true;
            }

            return false;
        }

        public static List<InvestmentGoal> GetProductGoals(GameEntity product, GameContext Q, GameEntity controller)
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
                var coreLoyalty = Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(product));

                // has no goals at start
                if (product.completedGoals.Goals.Count == 0 || coreLoyalty < 5)
                    return GoalToList(new InvestmentGoalMakePrototype());

                if (Completed(product, InvestorGoalType.ProductPrototype, Q))
                    goals.Add(InvestorGoalType.ProductFirstUsers);

                if (Completed(product, InvestorGoalType.ProductFirstUsers, Q) && coreLoyalty < marketFit)
                    goals.Add(InvestorGoalType.ProductBecomeMarketFit);

                if (Completed(product, InvestorGoalType.ProductBecomeMarketFit, Q))
                    goals.Add(InvestorGoalType.ProductRelease);
            }

            if (releasedProduct)
            {
                if (Completed(product, InvestorGoalType.ProductRelease, Q))
                    goals.Add(InvestorGoalType.ProductStartMonetising);

                if (Completed(product, InvestorGoalType.ProductStartMonetising, Q))
                    goals.Add(InvestorGoalType.GrowUserBase);

                if (users > 500_000 && ourAudiences < amountOfAudiences)
                    goals.Add(InvestorGoalType.GainMoreSegments);
            }

            if (Marketing.IsHasDisloyalAudiences(product))
                return GoalToList(new InvestmentGoalRegainLoyalty());

            return WrapGoals(goals, product, Q, controller);
        }

        public static List<InvestmentGoal> GetGroupOnlyGoals(GameEntity company, GameContext Q, GameEntity controller)
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

            return WrapGoals(goals, company, Q, controller);
        }

        public static List<InvestmentGoal> GetCommonGoals(GameEntity company, GameContext Q, GameEntity controller)
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

            return WrapGoals(goals, company, Q, controller);
        }


        public static List<InvestmentGoal> GoalToList(InvestmentGoal goal) => new List<InvestmentGoal> { goal };
        public static List<InvestmentGoal> WrapGoals(List<InvestorGoalType> goals, GameEntity company, GameContext Q, GameEntity controller)
        {
            return goals
                .Where(g => !HasGoalAlready(company, Q, g))
                .Select(g => GetInvestmentGoal(company, Q, g).SetExecutorAndController(company.company.Id, controller.company.Id))
                .ToList();
        }
    }
}
