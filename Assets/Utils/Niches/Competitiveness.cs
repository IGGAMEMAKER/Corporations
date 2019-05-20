using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static BonusContainer GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            int marketLevel = 10;

            int productLevel = company.product.ProductLevel;

            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            return new BonusContainer("Product Competitiveness")
                .Append("Product Level", productLevel)
                .Append("Market Requirements", -marketLevel)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus);
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitivenessBonus(company, gameContext).Sum();
        }

        //public static string GetProductCompetitivenessDescription(GameEntity company, GameContext gameContext)
        //{

        //}
    }
}
