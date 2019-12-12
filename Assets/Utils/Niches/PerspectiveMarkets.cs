using System.Linq;

namespace Assets.Utils
{
    public static partial class Markets
    {
        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType) => IsPerspectiveNiche(GetNiche(gameContext, nicheType));
        public static bool IsPerspectiveNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase == MarketState.Trending;
        }

        public static bool IsInnovativeNiche(GameContext gameContext, NicheType nicheType) => IsInnovativeNiche(GetNiche(gameContext, nicheType));
        public static bool IsInnovativeNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase == MarketState.Innovation;
        }

        public static bool IsAdjacentToCompanyInterest(GameEntity niche, GameEntity company)
        {
            var industries = company.companyFocus.Industries;

            return company.companyFocus.Industries.Contains(niche.niche.IndustryType);
        }

        public static GameEntity[] GetPerspectiveAdjacentNiches(GameContext context, GameEntity group)
        {
            return GetPerspectiveNiches(context)
                .Where(n => IsAdjacentToCompanyInterest(n, group))
                .ToArray();
        }

        public static GameEntity[] GetPerspectiveNiches(GameContext context)
        {
            return GetPlayableNiches(context)
                .Where(IsPerspectiveNiche)
                .ToArray();
        }
    }
}
