using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void PayDividends(GameContext gameContext, int companyId)
        {
            PayDividends(gameContext, GetCompany(gameContext, companyId));
        }

        public static void PayDividends(GameContext gameContext, GameEntity company)
        {
            int dividendSize = 33;
            var balance = company.companyResource.Resources.money;

            var dividends = balance * dividendSize / 100;

            PayDividends(gameContext, company, dividends);
        }

        public static void PayDividends(GameContext gameContext, GameEntity company, long dividends)
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                var sharePercentage = GetShareSize(gameContext, company.company.Id, investorId);

                var sum = dividends * sharePercentage / 100;

                AddMoneyToInvestor(gameContext, investorId, sum);
            }

            SpendResources(company, dividends);
        }
    }
}
