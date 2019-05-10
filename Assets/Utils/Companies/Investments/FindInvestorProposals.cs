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

            return totalShareholders;

            return Array.FindAll(totalShareholders, s => IsInvestorSuitable(s.shareholder, c));
        }

        static bool IsInvestorSuitableByGoal(InvestorType shareholderType, InvestorGoal goal)
        {
            switch (goal)
            {
                case InvestorGoal.BecomeMarketFit:
                    return shareholderType == InvestorType.Angel;

                case InvestorGoal.BecomeProfitable:
                    return shareholderType == InvestorType.VentureInvestor;

                case InvestorGoal.GrowClientBase:
                    return shareholderType == InvestorType.VentureInvestor;

                case InvestorGoal.GrowCompanyCost:
                    return shareholderType == InvestorType.Strategic;

                case InvestorGoal.GrowProfit:
                    return shareholderType == InvestorType.StockExchange;

                default:
                    return shareholderType == InvestorType.FFF;
            }
        }

        public static bool IsInvestorSuitable(ShareholderComponent shareholder, GameEntity company)
        {
            bool isSuitableByGoal = IsInvestorSuitableByGoal(shareholder.InvestorType, company.companyGoal.InvestorGoal);

            return isSuitableByGoal;
        }
    }
}
