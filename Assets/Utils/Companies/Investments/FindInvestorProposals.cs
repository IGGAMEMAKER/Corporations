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
        
        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal, int expires)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                // scum goals. they don't count
                case InvestorGoal.Prototype: measurableGoal = 1; break;
                case InvestorGoal.FirstUsers: measurableGoal = 500; break;

                case InvestorGoal.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoal.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoal.IPO: measurableGoal = 1; break;

                case InvestorGoal.GrowCompanyCost:
                    measurableGoal = CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id) * (100 + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    break;
            }

            company.ReplaceCompanyGoal(investorGoal, expires, measurableGoal);
            LockCompanyGoal(gameContext, company);
        }

        public static void LockCompanyGoal(GameContext gameContext, GameEntity company)
        {
            AddCooldown(gameContext, company, CooldownType.CompanyGoal, 365);
        }

        public static bool IsCanGoPublic(GameContext gameContext, int companyId)
        {
            bool isAlreadyPublic = GetCompanyById(gameContext, companyId).isPublicCompany;
            bool meetsIPORequirements = IsMeetsIPORequirements(gameContext, companyId);

            return !isAlreadyPublic && meetsIPORequirements;
        }
    }
}
