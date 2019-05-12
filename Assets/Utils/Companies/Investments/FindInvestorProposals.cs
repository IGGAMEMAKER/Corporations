using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static GameEntity[] GetPossibleInvestors(GameContext gameContext, int companyId)
        {
            var totalShareholders = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId);

            return Array.FindAll(totalShareholders, s => IsInvestorSuitable(s.shareholder, c));
        }

        public static bool IsInvestorSuitable(ShareholderComponent shareholder, GameEntity company)
        {
            return InvestmentUtils.IsInvestorSuitable(shareholder, company);
        }

        internal static void SetCompanyGoal(GameEntity company, InvestorGoal investorGoal, int expires)
        {
            company.ReplaceCompanyGoal(investorGoal, expires, 0);
        }
    }
}
