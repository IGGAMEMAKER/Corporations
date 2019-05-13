using Entitas;
using System;
using UnityEngine;

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



        internal static void SetCompanyGoal(GameContext gameContext, GameEntity company, InvestorGoal investorGoal, int expires)
        {
            long measurableGoal = 5000;

            switch (investorGoal)
            {
                // scum goals. they don't count
                case InvestorGoal.BecomeMarketFit: measurableGoal = -1; break;
                case InvestorGoal.BecomeProfitable: measurableGoal = 0; break;
                case InvestorGoal.IPO: measurableGoal = 1; break;

                case InvestorGoal.GrowCompanyCost:
                    measurableGoal = CompanyEconomyUtils.GetCompanyCost(gameContext, company.company.Id) * (100 + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST) / 100;
                    Debug.Log("Set Grow companyCost: " + measurableGoal);
                    break;

                //case InvestorGoal.GrowProfit:
                //    long change = CompanyEconomyUtils.GetBalanceChange(gameContext, company.company.Id);
                //    measurableGoal = change < * (100 + Constants.INVESTMENT_GOAL_GROWTH_REQUIREMENT_PROFIT_GROWTH) / 100;
                //    Debug.Log("Set Grow GrowProfit: " + measurableGoal);
                //    break;
            }

            company.ReplaceCompanyGoal(investorGoal, expires, measurableGoal);
        }
    }
}
