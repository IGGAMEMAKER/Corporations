using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToBuy(GameEntity buyer, GameEntity target)
        {
            return 1;
        }

        public static long GetStartupAttractiveness(GameContext gameContext, GameEntity startup)
        {
            long score = 0;

            if (NicheUtils.IsPerspectiveNiche(gameContext, startup.product.Niche))
                score += 100;
            else
                score -= 100;

            var positionOnMarket = NicheUtils.GetPositionOnMarket(gameContext, startup) + 1;
            score += 100 / positionOnMarket;

            var quality = NicheUtils.GetAppQualityOnMarket(gameContext, startup) + 1;
            score += 30 / quality;

            score += startup.branding.BrandPower;

            return score;
        }

        public static long GetDesireToBuyStartupAsGroup(GameContext gameContext, GameEntity group, GameEntity startup)
        {
            long score = 0;

            if (IsInSphereOfInterest(group, startup))
                score += 1000;

            var attractiveness = GetStartupAttractiveness(gameContext, startup);

            return score;
        }
    }
}
