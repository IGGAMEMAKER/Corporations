using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        internal static void SetFinancing(GameContext gameContext, int companyId, MarketingFinancing marketingFinancing)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var f = c.finance;

            c.ReplaceFinance(f.price, marketingFinancing, f.salaries, f.basePrice);
        }

        public static int GetMarketDiff(GameContext gameContext, int companyId)
        {
            var best = NicheUtils.GetLeaderApp(gameContext, companyId);

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return best.product.ProductLevel - c.product.ProductLevel;
        }

        public static long GetClients(GameEntity company)
        {
            long amount = 0;

            foreach (var p in company.marketing.Segments)
                amount += p.Value;

            return amount;
        }
    }
}
