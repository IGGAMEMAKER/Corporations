namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSellCompany(GameEntity company, GameContext gameContext)
        {
            var shareholders = company.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                desireToSell += GetDesireToSellShares(company, gameContext, invId, block.InvestorType) * block.amount;
                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }

        public static bool IsWillSellCompany(GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSellCompany(target, gameContext);

            //Debug.Log("IsWillSellCompany: " + target.company.Name + " - " + desire + "%");

            return desire > 75 || target.isOnSales || IsCompanyRelatedToPlayer(gameContext, target);
        }


        public static long GetDesireToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            return isProduct ? GetDesireToSellStartupByInvestorType(company, investorType, investorId, gameContext)
                : GetDesireToSellGroupByInvestorType(company, investorType, investorId, gameContext);
        }

        public static bool IsWantsToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            var desire = GetDesireToSellShares(company, gameContext, investorId, investorType);

            return desire > 0;
        }

        public static string GetDesireToSellDescriptionByInvestorType(GameEntity company, GameContext gameContext, int investorId)
        {
            var investor = GetInvestorById(gameContext, investorId);

            return GetSellRejectionDescriptionByInvestorType(investor.shareholder.InvestorType);
        }

        public static string GetSellRejectionDescriptionByInvestorType(InvestorType investorType)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return "Company goals are not completed";

                case InvestorType.Founder:
                    return "Founder ambitions not fulfilled";

                case InvestorType.Strategic:
                    return "Views this company as strategic ";

                default:
                    return investorType.ToString() + " will not sell shares";
            }
        }


        static int GetSeedValue()
        {
            return System.DateTime.Now.Hour;
        }
    }
}
