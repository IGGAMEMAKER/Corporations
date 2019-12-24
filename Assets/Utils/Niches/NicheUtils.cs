using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Markets
    {
        public static GameEntity[] GetNiches(GameContext context) => context.GetEntities(GameMatcher.Niche);

        public static GameEntity GetNiche(GameContext context, GameEntity product) => GetNiche(context, product.product.Niche);
        public static GameEntity GetNiche(GameContext context, NicheType nicheType)
        {
            var e = Array.Find(GetNiches(context), n => n.niche.NicheType == nicheType);

            if (e == null)
                e = CreateNicheMockup(nicheType, context);

            return e;
        }

        public static GameEntity[] GetIndustries(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Industry);
        }

        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(GetNiches(context), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNiches(context);

            return Array.FindAll(niches, n => n.niche.IndustryType == industry);
        }




        public static GameEntity[] GetPlayableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);

            return Array.FindAll(niches, IsPlayableNiche);
        }

        public static GameEntity[] GetObservableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);

            return Array.FindAll(niches, IsObservableNiche);
        }

        public static GameEntity[] GetPlayableNiches(GameContext context)
        {
            return GetNiches(context)
                .Where(IsPlayableNiche)
                .ToArray();
        }



        public static bool IsPlayableNiche(GameContext gameContext, NicheType nicheType) => IsPlayableNiche(GetNiche(gameContext, nicheType));
        public static bool IsPlayableNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase != MarketState.Idle && phase != MarketState.Death;
        }

        public static bool IsObservableNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return
                phase == MarketState.Trending ||
                phase == MarketState.Decay ||
                phase == MarketState.MassGrowth;
        }

        public static bool IsAppropriateStartNiche(GameEntity niche, GameContext gameContext)
        {
            var profile = niche.nicheBaseProfile.Profile;

            var isPerspective = IsIdleNicheReadyToStart(niche, gameContext);
            var isCheapToMaintain = profile.AppComplexity < AppComplexity.Hard;

            var isTopMarket = profile.AudienceSize == AudienceSize.Global; // GetMarketPotentialRating(niche) > 3;

            //var competition = GetCompetitorsAmount(niche.niche.NicheType, gameContext);

            return isPerspective && isCheapToMaintain && !isTopMarket;
        }
    }
}
