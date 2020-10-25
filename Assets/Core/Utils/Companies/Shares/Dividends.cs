using System;
using System.Collections.Generic;

namespace Assets.Core
{
    partial class Companies
    {
        public static void PayDividends(GameContext gameContext, GameEntity company, long dividends)
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                var sharePercentage = GetShareSize(gameContext, company, investorId);

                var sum = dividends * sharePercentage / 100;

                var investor = GetInvestorById(gameContext, investorId);
                AddResources(investor, sum, "Pay dividends");
            }

            SpendResources(company, dividends, "dividends");
        }
    }
}
