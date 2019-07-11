using Entitas;
using System;
using System.Linq;

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

        //public static long GetDesireToSell(GameEntity buyer, GameEntity target, GameContext gameContext)
        //{
        //    if (buyer.isManagingCompany && target.hasProduct)
        //        return GetDesireToSellStartup(target, gameContext);

        //    return 0;
        //}

        public static bool IsWillSellCompany(GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSell(target, gameContext);

            return desire > 75;
        }

        public static bool IsWillBuyCompany(GameEntity buyer, GameEntity target)
        {
            return GetDesireToBuy(buyer, target) > 0;
        }
    }
}
