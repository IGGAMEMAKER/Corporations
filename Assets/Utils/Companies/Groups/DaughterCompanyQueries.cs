using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static GameEntity[] GetDaughterUnhappyCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && p.team.Morale < 30)
            .ToArray();
        }

        public static GameEntity[] GetDaughterOutdatedCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && ProductUtils.IsOutOfMarket(p, gameContext))
            .ToArray();
        }

        public static GameEntity[] GetDaughterUpgradableCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && ProductUtils.IsHasAvailableProductImprovements(p))
            .ToArray();
        }
    }
}
