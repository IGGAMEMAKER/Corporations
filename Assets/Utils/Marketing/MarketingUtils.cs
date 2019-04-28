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
            int improvements = GetSegmentImprovementsBonus(gameContext, companyId, userType);
            int pricing = GetPricingLoyaltyPenalty(gameContext, companyId);
            int marketSituation = GetMarketSituationLoyaltyBonus(gameContext, companyId);

            return app + improvements - bugs - pricing + marketSituation;
        }

        public static int GetSegmentImprovementsBonus(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.Segments[userType] * 3;
        }

        public static int GetAppLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.ProductLevel * 4;
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
            stringBuilder.AppendFormat("\nImprovements: +{0}", GetSegmentImprovementsBonus(gameContext, companyId, userType));
            stringBuilder.AppendFormat("\n{0}", GetMarketSituationLoyaltyDescription(gameContext, companyId));
            stringBuilder.AppendFormat("\nBugs: -{0}", GetClientLoyaltyBugPenalty(gameContext, companyId));
            stringBuilder.AppendFormat("\nPricing: -{0}", GetPricingLoyaltyPenalty(gameContext, companyId));

            return stringBuilder.ToString();
        }

        public static string GetMarketSituationLoyaltyDescription(GameContext gameContext, int companyId)
        {
            var best = NicheUtils.GetLeaderApp(gameContext, companyId);

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var diff = best.product.ProductLevel - c.product.ProductLevel;

            var loyaltyBonus = GetMarketSituationLoyaltyBonus(gameContext, companyId);

            if (diff == 0)
                return $"Is Innovative: +{loyaltyBonus}";

            if (diff == 1)
                return "";

            return $"Out of market by {diff}LVL: {loyaltyBonus}";
        }

        public static int GetMarketDiff(GameContext gameContext, int companyId)
        {
            var best = NicheUtils.GetLeaderApp(gameContext, companyId);

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return best.product.ProductLevel - c.product.ProductLevel;
        }

        public static int GetMarketSituationLoyaltyBonus(GameContext gameContext, int companyId)
        {
            int diff = GetMarketDiff(gameContext, companyId);

            return 10 - 10 * diff;
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
