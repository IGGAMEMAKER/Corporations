using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheLifecyclePhase GetMarketState(GameEntity niche)
        {
            return niche.nicheState.Phase;
        }

        internal static bool IsExploredMarket(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNiche(gameContext, nicheType);

            return niche.hasResearch;
        }

        public static NicheLifecyclePhase GetMarketState(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNiche(gameContext, nicheType);

            return GetMarketState(niche);
        }

        public static int GetMarketRating(GameContext gameContext, NicheType niche)
        {
            return GetMarketRating(GetNiche(gameContext, niche));
        }

        public static int GetMarketRating(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            switch (phase)
            {
                case NicheLifecyclePhase.Idle: return 1;
                case NicheLifecyclePhase.Innovation: return 3;
                case NicheLifecyclePhase.Trending: return 4;
                case NicheLifecyclePhase.MassUse: return 5;
                case NicheLifecyclePhase.Decay: return 2;

                default:
                    return 0;
            }
        }

        public static int GetMarketPotentialRating(GameEntity niche)
        {
            var rating = 1;


            var profile = niche.nicheBaseProfile.Profile;

            var audience = profile.AudienceSize;
            var income = profile.Margin;

            if (audience == AudienceSize.Global || audience == AudienceSize.BigEnterprise) rating += 2;
            if (audience == AudienceSize.Million100 || audience == AudienceSize.SmallEnterprise) rating += 1;


            if (income == Margin.High) rating += 2;
            if (income == Margin.Mid) rating += 1;

            return rating;
        }

    }
}
