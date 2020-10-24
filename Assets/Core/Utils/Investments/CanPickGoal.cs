using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool IsPickableGoal(GameEntity company, GameContext gameContext, InvestorGoalType goal)
        {
            var users = Marketing.GetUsers(company);
            var cost = Economy.CostOf(company, gameContext);

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            var income = Economy.GetIncome(gameContext, company);

            bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(company);

            var minLoyalty = 5;
            var marketFit = 10; // 10 cause it allows monetisation for ads

            List<InvestorGoalType> RedoableGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.GrowIncome, InvestorGoalType.GrowUserBase, InvestorGoalType.GainMoreSegments,
                InvestorGoalType.OutcompeteCompanyByUsers, InvestorGoalType.OutcompeteCompanyByMarketShare, InvestorGoalType.OutcompeteCompanyByIncome,
                InvestorGoalType.AcquireCompany, InvestorGoalType.GrowCompanyCost
            };

            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.ProductRelease, InvestorGoalType.ProductBecomeMarketFit, InvestorGoalType.ProductFirstUsers, InvestorGoalType.ProductPrototype
            };

            // this goal was done already && cannot be done twice
            if (company.completedGoals.Goals.Contains(goal) && OneTimeGoals.Contains(goal))
                return false;

            if (company.completedGoals.Goals.Count == 0)
                return goal == InvestorGoalType.ProductPrototype;

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

                //case InvestorGoalType.Prototype:
                //    return focusedProduct && isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < minLoyalty;

                case InvestorGoalType.ProductBecomeMarketFit:
                    return isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.ProductFirstUsers:
                    return isPrototype && users < 2000;

                case InvestorGoalType.ProductRelease:
                    return isPrototype;

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
                    return !profitable;

                case InvestorGoalType.None:
                    return false;

                default:
                    return isGroup;
            }
        }
    }
}
