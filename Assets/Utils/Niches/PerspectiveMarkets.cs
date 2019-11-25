using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType) => IsPerspectiveNiche(GetNiche(gameContext, nicheType));
        public static bool IsPerspectiveNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase == NicheLifecyclePhase.Trending;
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
