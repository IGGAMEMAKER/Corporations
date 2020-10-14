using System;
using System.Collections.Generic;

namespace Assets.Core
{
    partial class Companies
    {
        public static void PayDividends(GameContext gameContext, int companyId) => PayDividends(gameContext, Get(gameContext, companyId));
        public static void PayDividends(GameContext gameContext, GameEntity company)
        {
            int dividendSize = 33;
            var balance = Economy.BalanceOf(company);

            var dividends = balance * dividendSize / 100;

            PayDividends(gameContext, company, dividends);
        }

        public static void PayDividends(GameContext gameContext, GameEntity company, long dividends)
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                var sharePercentage = GetShareSize(gameContext, company, investorId);

                var sum = dividends * sharePercentage / 100;

                AddMoneyToInvestor(gameContext, investorId, sum);
            }

            SpendResources(company, dividends);
        }

        //public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;
    }
}
