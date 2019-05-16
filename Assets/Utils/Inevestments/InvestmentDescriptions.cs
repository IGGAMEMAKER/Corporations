namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        internal static string GetInvestorOpinionDescription(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            int opinion = GetInvestorOpinion(gameContext, company, investor);

            string title = VisualUtils.Describe(
                "They will invest in this company if asked",
                "They will not invest",
                opinion
            );

            var description = new BonusContainer("Investor opinion", opinion);

            if (company.hasProduct)
                description.Append("Product competitiveness", NicheUtils.GetProductCompetitivenessBonus(company));

            description.Append("Same goals", 25);

            return title + "\n" + description;
        }

        public static string GetFormattedInvestorGoal(InvestorGoal investorGoal)
        {
            switch (investorGoal)
            {
                case InvestorGoal.BecomeMarketFit:
                    return "Become market fit";

                case InvestorGoal.BecomeProfitable:
                    return "Become profitable";

                case InvestorGoal.GrowCompanyCost:
                    return "Grow company cost by " + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST + "%";

                case InvestorGoal.IPO:
                    return "IPO";

                //case InvestorGoal.GrowProfit:
                //    return "Grow profit by " + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_PROFIT_GROWTH + "%";

                //case InvestorGoal.BecomeBestByTech:
                //    return "Become technology leader";

                //case InvestorGoal.GrowClientBase:
                //    return "Grow client base";

                //case InvestorGoal.ProceedToNextRound:
                //    return "Proceed to next investment round";
                default:
                    return investorGoal.ToString();
            }
        }

        public static string GetFormattedInvestorType(InvestorType investorType)
        {
            switch (investorType)
            {
                case InvestorType.Angel: return "Angel investor";
                case InvestorType.FFF: return "Family friends fools";
                case InvestorType.Founder: return "Founder";
                case InvestorType.StockExchange: return "Stock Exchange";
                case InvestorType.Strategic: return "Strategic investor";
                case InvestorType.VentureInvestor: return "Venture investor";
            }

            return investorType.ToString();
        }
    }
}
