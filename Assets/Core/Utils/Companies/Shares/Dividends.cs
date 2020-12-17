namespace Assets.Core
{
    partial class Companies
    {
        public static void PayDividends(GameContext gameContext, GameEntity company, long dividends)
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                
                var investor = GetInvestorById(gameContext, investorId);
                var sharePercentage = GetShareSize(gameContext, company, investor);

                var sum = dividends * sharePercentage / 100;

                AddResources(investor, sum, "Pay dividends");
            }

            SpendResources(company, dividends, "dividends");
        }
    }
}
