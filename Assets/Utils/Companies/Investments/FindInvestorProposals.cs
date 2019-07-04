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


        internal static bool IsInSphereOfInterest(GameEntity company, NicheType niche)
        {
            return company.companyFocus.Niches.Contains(niche);
        }

        internal static bool IsInSphereOfInterest(GameEntity company, GameEntity interestingCompany)
        {
            if (!interestingCompany.hasProduct)
                return false;

            return IsInSphereOfInterest(company, interestingCompany.product.Niche);
        }


        public static GameEntity[] GetPotentialInvestorsWhoAreReadyToInvest(GameContext gameContext, int companyId)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId);

            return Array.FindAll(investors, s => InvestmentUtils.IsInvestorSuitable(s, c) && InvestmentUtils.GetInvestorOpinion(gameContext, c, s) > 0);
        }

        public static bool IsCanGoPublic(GameContext gameContext, int companyId)
        {
            bool isAlreadyPublic = GetCompanyById(gameContext, companyId).isPublicCompany;
            bool meetsIPORequirements = IsMeetsIPORequirements(gameContext, companyId);

            return !isAlreadyPublic && meetsIPORequirements;
        }
    }
}
