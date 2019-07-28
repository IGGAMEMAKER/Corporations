using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheLifecyclePhase GetMarketState(GameContext gameContext, NicheType nicheType)
        {
            return GetNicheEntity(gameContext, nicheType).nicheState.Phase;
        }

        public static int GetMarketRating(GameContext gameContext, NicheType niche)
        {
            return GetMarketRating(GetNicheEntity(gameContext, niche));
        }

        public static int GetMarketRating(GameEntity niche)
        {
            switch (niche.nicheState.Phase)
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

    }
}
