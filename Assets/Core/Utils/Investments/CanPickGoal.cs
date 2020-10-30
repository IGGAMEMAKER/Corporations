using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {
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

        public static List<InvestmentGoal> GetNewGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestmentGoal>();

            foreach (var goalType in (InvestorGoalType[])System.Enum.GetValues(typeof(InvestorGoalType)))
            {
                if (Investments.HasGoalAlready(company, Q, goalType))
                    continue;

                GameEntity realCompany = GetGoalPickingCompany(company, Q, goalType);

                bool isProduct = realCompany.hasProduct;


                // this goal was done already && cannot be done twice
                if (Investments.IsGoalDone(realCompany, goalType, Q))
                {
                    continue;
                }

                if (realCompany.completedGoals.Goals.Count == 0 && isProduct)
                {
                    // ensure, that making prototype is first goal
                    goals.Add(GetInvestmentGoal(company, Q, goalType));

                    return goals;
                }


                var users = Marketing.GetUsers(realCompany);
                var cost = Economy.CostOf(realCompany, Q);
                var income = Economy.GetIncome(Q, realCompany);

                bool isGroup = !isProduct;

                bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(realCompany);

                var marketFit = 10; // 10 cause it allows monetisation for ads


                bool isPrototype = isProduct && !realCompany.isRelease && focusedProduct;
                bool releasedProduct = isProduct && realCompany.isRelease;

                bool profitable = Economy.IsProfitable(Q, realCompany);

                var strongerCompetitorId = Companies.GetStrongerCompetitorId(realCompany, Q);
                bool hasStrongerCompanies = strongerCompetitorId >= 0;

                bool solidCompany = (releasedProduct || isGroup) && profitable && income > 500_000;

                bool hasWeakerCompanies = !hasStrongerCompanies; // is dominant on market

                if (IsPickableGoal(company, Q, goalType, 
                    isPrototype, realCompany, releasedProduct, isProduct, income, users, solidCompany,
                    hasStrongerCompanies, hasWeakerCompanies, isGroup, profitable, cost, marketFit))
                {
                    //Debug.Log("New goal: " + e);
                    goals.Add(GetInvestmentGoal(company, Q, goalType));
                }
            }

            return goals;
        }

        public static bool IsPickableGoal(
            GameEntity company, GameContext Q, InvestorGoalType goalType,

            bool isPrototype, GameEntity realCompany, bool releasedProduct, bool isProduct, long income, long users,
            bool solidCompany, bool hasStrongerCompanies, bool hasWeakerCompanies,
            bool isGroup, bool profitable, long cost, int marketFit
            )
        {
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
                    return isPrototype && IsGoalDone(realCompany, InvestorGoalType.ProductPrototype, Q) 
                        && Marketing.GetSegmentLoyalty(realCompany, Marketing.GetCoreAudienceId(realCompany)) < marketFit;

                case InvestorGoalType.ProductFirstUsers:
                    return isPrototype && IsGoalDone(realCompany, InvestorGoalType.ProductBecomeMarketFit, Q); // && users < 100_000

                case InvestorGoalType.ProductRelease:
                    return isPrototype && IsGoalDone(realCompany, InvestorGoalType.ProductFirstUsers, Q);

                case InvestorGoalType.StartMonetising:
                    return releasedProduct && IsGoalDone(realCompany, InvestorGoalType.ProductRelease, Q);

                case InvestorGoalType.ProductRegainLoyalty:
                    return isProduct && Marketing.IsHasDisloyalAudiences(realCompany);

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && IsGoalDone(realCompany, InvestorGoalType.StartMonetising, Q);

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && income > 50_000;

                case InvestorGoalType.GainMoreSegments:
                    var amountOfAudiences = Marketing.GetAudienceInfos().Count;
                    var ourAudiences = Marketing.GetAmountOfTargetAudiences(realCompany);

                    return releasedProduct && users > 500_000 && ourAudiences < amountOfAudiences;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !realCompany.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

                // BOTH
                case InvestorGoalType.GrowCompanyCost:
                    return solidCompany;

                case InvestorGoalType.OutcompeteCompanyByIncome:
                    return solidCompany && hasStrongerCompanies;

                case InvestorGoalType.OutcompeteCompanyByUsers:
                    return solidCompany && hasStrongerCompanies;

                case InvestorGoalType.OutcompeteCompanyByCost:
                    return solidCompany && hasStrongerCompanies;

                case InvestorGoalType.AcquireCompany:
                    //Debug.Log("Check ACQUIRE COMPANY: solid=" + solidCompany + " has targets=" + hasWeakerCompanies);
                    return solidCompany && hasWeakerCompanies;

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
