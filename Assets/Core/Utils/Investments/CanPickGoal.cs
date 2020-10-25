using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool IsPickableGoal(GameEntity company, GameContext gameContext, InvestorGoalType goal)
        {
            bool isProduct = company.hasProduct;
            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductRelease,
                InvestorGoalType.ProductBecomeMarketFit,
                InvestorGoalType.ProductFirstUsers,
                InvestorGoalType.ProductPrototype
            };

            // this goal was done already && cannot be done twice
            if (company.completedGoals.Goals.Contains(goal) && OneTimeGoals.Contains(goal))
                return false;

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

            var strongerCompetitorId = GetStrongerCompetitorId(company, gameContext);
            bool hasStrongerOpposition = strongerCompetitorId >= 0;

            bool solidCompany = (releasedProduct || isGroup) && profitable && income > 1_000_000;

            bool hasWeakerCompanies = !hasStrongerOpposition; // is dominant on market
            


            switch (goal)
            {
                // PRODUCTS

                case InvestorGoalType.ProductPrototype:
                    return isPrototype; // && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < minLoyalty

                case InvestorGoalType.ProductBecomeMarketFit:
                    return isPrototype && Done(company, InvestorGoalType.ProductPrototype) && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.ProductFirstUsers:
                    return isPrototype && Done(company, InvestorGoalType.ProductBecomeMarketFit) && users < 2000;

                case InvestorGoalType.ProductRelease:
                    return isPrototype && Done(company, InvestorGoalType.ProductFirstUsers);

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && users > 50_000;

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && profitable && income > 100_000;

                case InvestorGoalType.GainMoreSegments:
                    return releasedProduct && users > 1_000_000 && company.marketing.ClientList.Count < Marketing.GetAudienceInfos().Count;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !company.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

                // BOTH
                case InvestorGoalType.GrowCompanyCost:
                    return solidCompany;

                case InvestorGoalType.OutcompeteCompanyByIncome:
                    return solidCompany && hasStrongerOpposition;

                case InvestorGoalType.OutcompeteCompanyByMarketShare:
                    return solidCompany && hasStrongerOpposition;

                case InvestorGoalType.AcquireCompany:
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
