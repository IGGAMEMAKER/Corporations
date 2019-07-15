using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSell(GameEntity company, GameContext gameContext)
        {
            if (company.hasProduct)
                return GetDesireToSellStartup(company, gameContext);
            else
                return GetDesireToSellGroup(company, gameContext);
        }

        public static bool IsWillSellCompany(GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSell(target, gameContext);

            Debug.Log("IsWillSellCompany: " + target.company.Name + " - " + desire + "%");

            return desire > 75;
        }

        public static bool IsWillBuyCompany(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            return GetDesireToBuy(buyer, target, gameContext) > 0;
        }


        public static string GetDesireToSellDescriptionByInvestorType(GameEntity company, GameContext gameContext, int investorId)
        {
            var investor = GetInvestorById(gameContext, investorId);

            return GetDesireToSellByInvestorType(company, gameContext, investorId, investor.shareholder.InvestorType);
        }

        public static long GetDesireToSellShares(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            return isProduct ? GetDesireToSellStartupByInvestorType(company, investorType, investorId, gameContext)
                : GetDesireToSellGroupByInvestorType(company, investorType, investorId, gameContext);
        }

        public static string GetDesireToSellByInvestorType(GameEntity company, GameContext gameContext, int investorId, InvestorType investorType)
        {
            bool isProduct = company.hasProduct;

            var desire = GetDesireToSellShares(company, gameContext, investorId, investorType);
            bool wantsToSell = desire > 0;

            if (wantsToSell || investorType == InvestorType.StockExchange)
                return Visuals.Positive("Wants to sell shares!");

            var text = "";

            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    text = "Company goals are not completed";
                    break;

                case InvestorType.Founder:
                    text = "Founder ambitions not fulfilled";
                    break;

                case InvestorType.Strategic:
                    text = "Views this company as strategic ";
                    break;

                default:
                    text = investorType.ToString() + " will not sell shares";
                    break;
            }

            return Visuals.Negative(text);
        }

        //public static string GetSellingRejectionDescriptionByInvestorType()
    }
}
