namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static bool IsWillSellCompany(GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSellCompany(target, gameContext);
            var founderWantsToSell = GetFounderAmbition(target, gameContext) == Ambition.EarnMoney;

            return desire > 75 || founderWantsToSell || target.isOnSales || IsCompanyRelatedToPlayer(gameContext, target);
        }

        public static long GetDesireToSellCompany(GameEntity company, GameContext gameContext)
        {
            var shareholders = company.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                desireToSell += GetBaseDesireToSellShares(gameContext, company, invId, block.InvestorType) * block.amount;
                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }


        public static bool IsYoungCompany(GameEntity company)
        {
            return company.metricsHistory.Metrics.Count < 6;
        }

        public static long GetBaseDesireToSellShares(GameContext gameContext, int companyId, int shareholderId) => GetBaseDesireToSellShares(gameContext, GetCompanyById(gameContext, companyId), shareholderId, GetInvestorById(gameContext, shareholderId));
        public static long GetBaseDesireToSellShares(GameContext gameContext, GameEntity company, int shareholderId, GameEntity investor) => GetBaseDesireToSellShares(gameContext, company, shareholderId, investor.shareholder.InvestorType);
        public static long GetBaseDesireToSellShares(GameContext gameContext, GameEntity company, int shareholderId, InvestorType investorType)
        {
            bool isProduct = company.hasProduct;

            var bonusContainer = new Bonus("Desire to sell");

            bonusContainer.Append("Base", -1);

            bonusContainer.AppendAndHideIfZero("Is young company", IsYoungCompany(company) ? -10 : 0);

            if (isProduct)
                bonusContainer.AppendAndHideIfZero("By investor type", GetDesireToSellStartupByInvestorType(company, investorType, shareholderId, gameContext));
            else
                bonusContainer.AppendAndHideIfZero("By investor type", GetDesireToSellGroupByInvestorType(company, investorType, shareholderId, gameContext));

            return bonusContainer.Sum();
        }

        public static bool IsWantsToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            var desire = GetBaseDesireToSellShares(gameContext, company, investorId, investorType);

            return desire > 0;
        }

        public static string GetSellRejectionDescriptionByInvestorType(InvestorType investorType, GameEntity company)
        {
            if (IsYoungCompany(company))
                return "It's too early to sell company";

            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return "Company goals are not completed";

                case InvestorType.Founder:
                    return "Founder ambitions not fulfilled";

                case InvestorType.Strategic:
                    return "Views this company as strategic";

                default:
                    return investorType.ToString() + " will not sell shares";
            }
        }
    }
}
