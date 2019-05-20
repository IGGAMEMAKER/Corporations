using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static BonusContainer GetProductCompetitiveness1(GameEntity company, GameContext gameContext)
        {
            int marketLevel = 10;

            int productLevel = company.product.ProductLevel;

            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            int competitiveness = productLevel - marketLevel + techLeadershipBonus;

            return new BonusContainer("Product Competitiveness", competitiveness)
                .Append("Product Level", productLevel)
                .Append("Market Requirements", -marketLevel)
                .Append(new BonusDescription { HideIfZero = true, Name = "Is Setting Trends", Value = techLeadershipBonus });
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitiveness1(company, gameContext).Sum();
        }

        //public static string GetProductCompetitivenessDescription(GameEntity company, GameContext gameContext)
        //{

        //}
    }
}
