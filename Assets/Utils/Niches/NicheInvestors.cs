using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static GameEntity[] GetInstitutionalInvestors(GameContext gameContext, GameEntity e)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            return Array.FindAll(investors, s => CompanyUtils.IsInSphereOfInterest(s, e.niche.NicheType));
        }
    }
}
