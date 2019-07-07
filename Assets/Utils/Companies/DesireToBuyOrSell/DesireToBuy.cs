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

        public static long GetDesireToBuyStartupAsGroup(GameEntity group, GameEntity startup)
        {
            long score = 0;

            if (IsInSphereOfInterest(group, startup))
                score += 1000;


        }

        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            var phase = niche.nicheState.Phase;

            return phase == NicheLifecyclePhase.Innovation && phase == NicheLifecyclePhase.Trending && phase == NicheLifecyclePhase.MassUse;
        }
    }
}
