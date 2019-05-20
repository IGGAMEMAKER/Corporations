namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        static BonusContainer GetClientLoyaltyBonus(GameContext gameContext, int companyId, UserType userType)
        {
            int app = GetAppLoyaltyBonus(gameContext, companyId);
            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            int pricing = GetClientLoyaltyPricingPenalty(gameContext, companyId);
            int marketRequirement = GetClientLoyaltyMarketSituationBonus(gameContext, companyId);

            return new BonusContainer("Client loyalty is")
                .Append("App level", app)
                .Append("Market demand", -marketRequirement)
                .Append("Bugs", -bugs)
                .Append("Pricing", -pricing);
        }

        public static long GetClientLoyalty(GameContext gameContext, int companyId, UserType userType)
        {
            return GetClientLoyaltyBonus(gameContext, companyId, userType).Sum();
        }

        public static string GetClientLoyaltyDescription(GameContext gameContext, int companyId, UserType userType)
        {
            return GetClientLoyaltyBonus(gameContext, companyId, userType).ToString();
        }

        public static int GetClientLoyaltyBugPenalty(GameContext gameContext, int companyId)
        {
            int bugs = 15;

            return 0;
        }

        public static int GetAppLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.ProductLevel * 4;
        }

        public static int GetClientLoyaltyMarketSituationBonus(GameContext gameContext, int companyId)
        {
            return 10 * 4;
        }

        public static int GetClientLoyaltyPricingPenalty(GameContext gameContext, int companyId)
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
    }
}
