namespace Assets.Utils
{
    public static partial class Investments
    {
        public static string GetFormattedInvestorGoal(InvestorGoal investorGoal)
        {
            switch (investorGoal)
            {
                case InvestorGoal.Prototype:
                    return "Create a prototype";

                case InvestorGoal.FirstUsers:
                    return "Get first users";

                case InvestorGoal.BecomeMarketFit:
                    return "Become market fit";

                case InvestorGoal.BecomeProfitable:
                    return "Become profitable";

                case InvestorGoal.GrowCompanyCost:
                    return "Grow company cost by " + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST + "%";

                case InvestorGoal.IPO:
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
