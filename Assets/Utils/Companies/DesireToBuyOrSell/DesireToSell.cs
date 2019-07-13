using UnityEngine;

namespace Assets.Utils
{
    public enum Ambition
    {
        EarnMoney,
        RuleProduct,
        
        CreateUnicorn,
        IPO,

        RuleCorporation
    }


    public static partial class CompanyUtils
    {
        public static long GetDesireToSell(GameEntity company, GameContext gameContext)
        {
            if (company.hasProduct)
                return GetDesireToSellStartup(company, gameContext);
            else
                return 0;
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
    }
}
