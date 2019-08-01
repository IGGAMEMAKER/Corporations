using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        static BonusContainer GetClientLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var app = GetClientLoyaltyAppPart(gameContext, companyId);
            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            int pricing = GetClientLoyaltyPricingPenalty(gameContext, companyId);

            bool isOnlyPlayer = NicheUtils.GetPlayersOnMarket(gameContext, companyId).Count() == 1;
            int onlyPlayerBonus = isOnlyPlayer ? 30 : 0;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var SegmentBonus = GetSegmentDevelopmentLoyaltyBonus(gameContext, companyId);

            return new BonusContainer("Client loyalty is")
                .RenderTitle()
                //.Append("Product Competitiveness", app)
                .Append("Improvements", SegmentBonus)
                .AppendAndHideIfZero("Is only company", onlyPlayerBonus)
                .Append("Pricing", -pricing)
                .AppendAndHideIfZero("Bugs", -bugs)
                ;
        }

        public static long GetSegmentDevelopmentLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.Concept;
        }

        public static long GetClientLoyalty(GameContext gameContext, int companyId)
        {
            return GetClientLoyaltyBonus(gameContext, companyId).Sum();
        }

        public static string GetClientLoyaltyDescription(GameContext gameContext, int companyId)
        {
            return GetClientLoyaltyBonus(gameContext, companyId).ToString();
        }

        public static int GetClientLoyaltyBugPenalty(GameContext gameContext, int companyId)
        {
            int bugs = 15;

            return 0;
        }

        public static long GetClientLoyaltyAppPart(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return NicheUtils.GetProductCompetitiveness(c, gameContext);
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
