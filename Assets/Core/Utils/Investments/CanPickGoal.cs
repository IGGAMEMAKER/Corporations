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

            foreach (var e in (InvestorGoalType[])System.Enum.GetValues(typeof(InvestorGoalType)))
            {
                if (Investments.IsPickableGoal(company, Q, e) && !Investments.HasGoalAlready(company, Q, e))
                {
                    //Debug.Log("New goal: " + e);
                    goals.Add(GetInvestmentGoal(company, Q, e));
                }
            }

            return goals;
        }

        public static bool IsPickableGoal(GameEntity company1, GameContext gameContext, InvestorGoalType goal)
        {
            GameEntity company = GetGoalPickingCompany(company1, gameContext, goal);

            bool isProduct = company.hasProduct;


            // this goal was done already && cannot be done twice
            if (Investments.Done(company, goal, gameContext))
            {
                Debug.Log("goal " + goal + " for " + company.company.Name + " was done or outgrown");
                return false;
            }

            if (company.completedGoals.Goals.Count == 0 && isProduct)
                return goal == InvestorGoalType.ProductPrototype;


            var users = Marketing.GetUsers(company);
            var cost = Economy.CostOf(company, gameContext);
            var income = Economy.GetIncome(gameContext, company);

            bool isGroup = !isProduct;

            bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(company);

            var marketFit = 10; // 10 cause it allows monetisation for ads


            bool isPrototype = isProduct && !company.isRelease && focusedProduct;
            bool releasedProduct = isProduct && company.isRelease;

            bool profitable = Economy.IsProfitable(gameContext, company);

            var strongerCompetitorId = Companies.GetStrongerCompetitorId(company, gameContext);
            bool hasStrongerCompanies = strongerCompetitorId >= 0;

            bool solidCompany = (releasedProduct || isGroup) && profitable && income > 500_000;

            bool hasWeakerCompanies = !hasStrongerCompanies; // is dominant on market

            switch (goal)
            {
                // PRODUCTS

                case InvestorGoalType.ProductPrototype:
                    return isPrototype;

                case InvestorGoalType.ProductBecomeMarketFit:
                    return isPrototype && Done(company, InvestorGoalType.ProductPrototype, gameContext) && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.ProductFirstUsers:
                    return isPrototype && Done(company, InvestorGoalType.ProductBecomeMarketFit, gameContext); // && users < 100_000

                case InvestorGoalType.ProductRelease:
                    return isPrototype && Done(company, InvestorGoalType.ProductFirstUsers, gameContext);

                case InvestorGoalType.StartMonetising:
                    return releasedProduct && Done(company, InvestorGoalType.ProductRelease, gameContext);

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && Done(company, InvestorGoalType.StartMonetising, gameContext);

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && income > 50_000;

                case InvestorGoalType.GainMoreSegments:
                    var amountOfAudiences = Marketing.GetAudienceInfos().Count;
                    var ourAudiences = Marketing.GetAmountOfTargetAudiences(company);

                    return releasedProduct && users > 500_000 && ourAudiences < amountOfAudiences;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !company.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

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

                //case InvestorGoalType.DiversifyIncome:
                //    if (releasedProduct && profitable && income > 500_000 && Products.GetMonetisationFeatures(company).Length )
                //    {

                //    }
                //    return (releasedProduct || isGroup) && profitable && income > 

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
