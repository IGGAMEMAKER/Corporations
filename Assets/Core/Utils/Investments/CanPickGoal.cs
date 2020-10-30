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

            if (isProduct)
                goals.AddRange(GetProductCompanyGoals(company, Q));

            if (isGroup)
            {
                var daughterProducts = Companies.GetDaughterProducts(Q, company);

                foreach (var d in daughterProducts)
                    goals.AddRange(GetProductCompanyGoals(d, Q));

                goals.AddRange(GetGroupOnlyGoals(company, Q));
            }

            goals.AddRange(GetBothGroupAndProductGoals(company, Q));

            return goals;
        }

        public static GameEntity GetGoalPickingCompany(GameEntity company1, GameContext gameContext, InvestorGoalType goalType)
        {
            GameEntity company = null;

            if (company1.hasProduct)
            {
                company = company1;
            }
            else
            {
                var daughterProducts = Companies.GetDaughterProducts(gameContext, company1);
                if (daughterProducts.Count() == 1)
                {
                    company = daughterProducts.First();
                }
                else
                {
                    company = company1;
                }
            }

            return company;
        }

        public static bool HasGoalAlready(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType);
        }
        // IsGoalDoneOrOutgrown
        public static bool Completed(GameEntity company1, InvestorGoalType goal, GameContext gameContext) => IsGoalDoneOrOutgrown(company1, goal, gameContext);
        public static bool IsGoalDoneOrOutgrown(GameEntity company1, InvestorGoalType goal, GameContext gameContext)
        {
            var company = GetGoalPickingCompany(company1, gameContext, goal);

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

        public static List<InvestmentGoal> WrapGoalToList(InvestmentGoal goal) => new List<InvestmentGoal> { goal };

        public static List<InvestmentGoal> GetProductCompanyGoals(GameEntity product, GameContext Q)
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

            bool focusedProduct = Marketing.IsFocusingOneAudience(product);
            var marketFit = 10; // 10 cause it allows monetisation for ads


            bool isPrototype = !product.isRelease && focusedProduct;
            bool releasedProduct = product.isRelease;

            long users = Marketing.GetUsers(product);

            var amountOfAudiences = Marketing.GetAudienceInfos().Count;
            var ourAudiences = Marketing.GetAmountOfTargetAudiences(product);

            if (isPrototype)
            {
                var coreLoyalty = Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(product));

                // has no goals at start
                if (product.completedGoals.Goals.Count == 0 || coreLoyalty < 5)
                    return WrapGoalToList(new InvestmentGoalMakePrototype());

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
                return WrapGoalToList(new InvestmentGoalRegainLoyalty());

            return WrapGoalList(goals, product, Q);
        }

        public static List<InvestmentGoal> WrapGoalList(List<InvestorGoalType> goals, GameEntity company, GameContext Q)
        {
            return goals
                .Where(g => !HasGoalAlready(company, Q, g))
                .Select(g => GetInvestmentGoal(company, Q, g))
                .ToList();
        }

        public static List<InvestmentGoal> GetGroupOnlyGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestorGoalType>();

            var groupGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.AcquireCompany,
                InvestorGoalType.IPO,
            };

            var income = Economy.GetIncome(Q, company);

            bool solidCompany = income > 500_000;

            var weakerCompany = Companies.GetWeakerCompetitor(company, Q);
            bool hasWeakerCompanies = weakerCompany != null;

            if (solidCompany && hasWeakerCompanies)
                goals.Add(InvestorGoalType.AcquireCompany);

            return WrapGoalList(goals, company, Q);
        }

        public static List<InvestmentGoal> GetBothGroupAndProductGoals(GameEntity company, GameContext Q)
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

            bool releasedProduct = company.hasProduct && company.isRelease;

            bool isGroup = !company.hasProduct;

            bool profitable = Economy.IsProfitable(Q, company);



            var income = Economy.GetIncome(Q, company);

            bool solidCompany = (releasedProduct || isGroup) && income > 500_000;

            var strongerCompetitor = Companies.GetStrongerCompetitor(company, Q);
            bool hasStrongerCompanies = strongerCompetitor != null;


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

            return WrapGoalList(goals, company, Q);
        }

        public static bool IsPickableGoal(
            GameEntity company, GameContext Q, InvestorGoalType goalType,

            bool isPrototype, GameEntity realCompany, bool releasedProduct, bool isProduct, long income, long users,
            bool solidCompany, bool hasStrongerCompanies, bool hasWeakerCompanies,
            bool isGroup, bool profitable, long cost, int marketFit
            )
        {
            Companies.Log(company, $"Trying to pick goal {goalType}, prototype={isPrototype}, released={releasedProduct} solid={solidCompany}");
            //GameEntity realCompany = GetGoalPickingCompany(company, Q, goalType);

            //bool isProduct = realCompany.hasProduct;


            //// this goal was done already && cannot be done twice
            //if (Investments.IsGoalDone(realCompany, goalType, Q))
            //{
            //    //Debug.Log("goal " + goal + " for " + company.company.Name + " was done or outgrown");
            //    return false;
            //}

            //if (realCompany.completedGoals.Goals.Count == 0 && isProduct)
            //    return goalType == InvestorGoalType.ProductPrototype;


            //var users = Marketing.GetUsers(realCompany);
            //var cost = Economy.CostOf(realCompany, Q);
            //var income = Economy.GetIncome(Q, realCompany);

            //bool isGroup = !isProduct;

            //bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(realCompany);

            //var marketFit = 10; // 10 cause it allows monetisation for ads


            //bool isPrototype = isProduct && !realCompany.isRelease && focusedProduct;
            //bool releasedProduct = isProduct && realCompany.isRelease;

            //bool profitable = Economy.IsProfitable(Q, realCompany);

            //var strongerCompetitorId = Companies.GetStrongerCompetitorId(realCompany, Q);
            //bool hasStrongerCompanies = strongerCompetitorId >= 0;

            //bool solidCompany = (releasedProduct || isGroup) && profitable && income > 500_000;

            //bool hasWeakerCompanies = !hasStrongerCompanies; // is dominant on market

            switch (goalType)
            {
                // PRODUCTS

                case InvestorGoalType.ProductPrototype:
                    return isPrototype;

                case InvestorGoalType.ProductBecomeMarketFit:
                    return isPrototype && Completed(realCompany, InvestorGoalType.ProductPrototype, Q) 
                        && Marketing.GetSegmentLoyalty(realCompany, Marketing.GetCoreAudienceId(realCompany)) < marketFit;

                case InvestorGoalType.ProductFirstUsers:
                    return isPrototype && Completed(realCompany, InvestorGoalType.ProductBecomeMarketFit, Q); // && users < 100_000

                case InvestorGoalType.ProductRelease:
                    return isPrototype && Completed(realCompany, InvestorGoalType.ProductFirstUsers, Q);

                case InvestorGoalType.ProductStartMonetising:
                    return releasedProduct && Completed(realCompany, InvestorGoalType.ProductRelease, Q);

                case InvestorGoalType.ProductRegainLoyalty:
                    return isProduct && Marketing.IsHasDisloyalAudiences(realCompany);

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && Completed(realCompany, InvestorGoalType.ProductStartMonetising, Q);

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && income > 50_000;

                case InvestorGoalType.GainMoreSegments:
                    var amountOfAudiences = Marketing.GetAudienceInfos().Count;
                    var ourAudiences = Marketing.GetAmountOfTargetAudiences(realCompany);

                    return releasedProduct && users > 500_000 && ourAudiences < amountOfAudiences;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !realCompany.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

                case InvestorGoalType.AcquireCompany:
                    //Debug.Log("Check ACQUIRE COMPANY: solid=" + solidCompany + " has targets=" + hasWeakerCompanies);
                    return solidCompany && hasWeakerCompanies;

                // BOTH
                case InvestorGoalType.GrowCompanyCost:
                    return solidCompany;

                case InvestorGoalType.OutcompeteCompanyByIncome:
                    return solidCompany && hasStrongerCompanies;

                case InvestorGoalType.OutcompeteCompanyByUsers:
                    return solidCompany && hasStrongerCompanies;

                case InvestorGoalType.OutcompeteCompanyByCost:
                    return solidCompany && hasStrongerCompanies;


                case InvestorGoalType.BecomeProfitable:
                    return solidCompany && !profitable;

                case InvestorGoalType.None:
                    return false;

                default:
                    return isGroup;
            }
        }
    }
}
