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
        public static long GetDesireToSell(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            if (buyer.isManagingCompany && target.hasProduct)
                return GetDesireToSellStartup(target, gameContext);

            return 0;
        }

        public static bool IsWillSellCompany(GameEntity buyer, GameEntity target, GameContext gameContext)
        {
            var desire = GetDesireToSell(buyer, target, gameContext);

            return desire > 75;
        }
    }
}
