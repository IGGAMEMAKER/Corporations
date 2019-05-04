using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static int GetClientLoyaltyBugPenalty(GameContext gameContext, int companyId)
        {
            int bugs = 15;

            return bugs;
        }

        public static int GetClientLoyalty(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            int app = GetAppLoyaltyBonus(gameContext, companyId);
            int pricing = GetPricingLoyaltyPenalty(gameContext, companyId);
            int marketRequirement = GetMarketSituationLoyaltyBonus(gameContext, companyId);

            return app - bugs - pricing - marketRequirement;
        }

        internal static void SetFinancing(GameContext gameContext, int companyId, MarketingFinancing marketingFinancing)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var f = c.finance;

            c.ReplaceFinance(f.price, marketingFinancing, f.salaries, f.basePrice);
        }

        public static int GetAppLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.ProductLevel * 4;
        }

        public static int GetMarketSituationLoyaltyBonus(GameContext gameContext, int companyId)
        {
            return 10 * 4;
        }

        public static int GetPricingLoyaltyPenalty(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var pricing = c.finance.price;

            switch (pricing)
            {
                case Pricing.Free: return 0;
                case Pricing.Low: return 5;
                case Pricing.Medium: return 22;
                case Pricing.High: return 30;

                default: return 1000;
            }
        }

        public static string GetClientLoyaltyDescription(GameContext gameContext, int companyId, UserType userType)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Current loyalty is {0} due to:", GetClientLoyalty(gameContext, companyId, userType));
            stringBuilder.AppendFormat("\nApp level: +{0}", GetAppLoyaltyBonus(gameContext, companyId));
            stringBuilder.AppendFormat("\nMarket demand: -{0}", GetMarketSituationLoyaltyBonus(gameContext, companyId));
            stringBuilder.AppendFormat("\nBugs: -{0}", GetClientLoyaltyBugPenalty(gameContext, companyId));
            stringBuilder.AppendFormat("\nPricing: -{0}", GetPricingLoyaltyPenalty(gameContext, companyId));

            return stringBuilder.ToString();
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
