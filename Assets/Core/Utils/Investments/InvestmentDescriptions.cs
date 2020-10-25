using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static string GetFormattedCompanyGoals(GameEntity company)
        {
            return string.Join("\n", company.companyGoal.Goals.Select(g => g.GetFormattedName()));
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
