using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static GameEntity[] GetPotentialInvestors(GameContext gameContext, int companyId)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId);

            return Array.FindAll(investors, s => InvestmentUtils.IsInvestorSuitable(s, c));
        }

        public static bool IsCanGoPublic(GameContext gameContext, int companyId)
        {
            bool isAlreadyPublic = GetCompanyById(gameContext, companyId).isPublicCompany;
            bool meetsIPORequirements = IsMeetsIPORequirements(gameContext, companyId);

            return !isAlreadyPublic && meetsIPORequirements;
        }
    }
}
