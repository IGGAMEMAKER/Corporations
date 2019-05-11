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

            //return totalShareholders;

            return Array.FindAll(totalShareholders, s => IsInvestorSuitable(s.shareholder, c));
        }

        

        public static bool IsInvestorSuitable(ShareholderComponent shareholder, GameEntity company)
        {
            bool isSuitableByGoal = InvestmentUtils.IsInvestorSuitableByGoal(shareholder.InvestorType, company.companyGoal.InvestorGoal);

            return isSuitableByGoal;
        }
    }
}
