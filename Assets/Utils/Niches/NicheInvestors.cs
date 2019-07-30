using Entitas;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static GameEntity[] GetInstitutionalInvestors(GameContext gameContext, GameEntity e)
        {
            return Array.FindAll(gameContext
                .GetEntities(GameMatcher.Shareholder),
                s => CompanyUtils.IsInSphereOfInterest(s, e.niche.NicheType)
                );
        }
    }
}
