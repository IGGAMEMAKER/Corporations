using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static string GetFormattedCompanyGoals(GameEntity company)
        {
            return string.Join("\n", company.companyGoal.Goals.Select(g => GetFormattedInvestorGoal(g.InvestorGoalType)));
        }
        public static string GetFormattedInvestorGoal(InvestorGoalType investorGoal)
        {
            switch (investorGoal)
            {
                case InvestorGoalType.ProductPrototype:
                    return "Create a prototype";

                case InvestorGoalType.ProductFirstUsers:
                    return "Get first users";

                case InvestorGoalType.ProductBecomeMarketFit:
                    return "Become market fit";

                case InvestorGoalType.BecomeProfitable:
                    return "Become profitable";

                case InvestorGoalType.GrowCompanyCost:
                    return "Grow company cost by " + C.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST + "%";

                case InvestorGoalType.IPO:
                    return "IPO";
                default:
                    return investorGoal.ToString();
            }
        }

        public static string GetFormattedInvestorType(InvestorType investorType)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                    return "Angel investor";

                case InvestorType.FFF:
                    return "Family, friends & fools";

                case InvestorType.Founder:
                    return "Founder";

                case InvestorType.StockExchange:
                    return "Stock Exchange";

                case InvestorType.Strategic:
                    return "Strategic investor";

                case InvestorType.VentureInvestor:
                    return "Venture investor";
                default:
                    return investorType.ToString();
            }

        }
    }
}
