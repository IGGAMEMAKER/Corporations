using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        static BonusContainer GetClientLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var app = GetClientLoyaltyRelativeStrength(gameContext, companyId);
            int pricing = GetClientLoyaltyPricingPenalty(gameContext, companyId);

            bool isOnlyPlayer = NicheUtils.GetProductsOnMarket(gameContext, companyId).Count() == 1;
            int onlyPlayerBonus = isOnlyPlayer ? 30 : 0;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return new BonusContainer("Client loyalty is")
                .RenderTitle()
                .Append("Product Competitiveness", app)
                .AppendAndHideIfZero("Is only company", onlyPlayerBonus)
                //.Append("Pricing", -pricing)
                ;
        }

        public static long GetClientLoyalty(GameContext gameContext, int companyId)
        {
            return GetClientLoyaltyBonus(gameContext, companyId).Sum();
        }
        
        public static long GetClientLoyaltyRelativeStrength(GameContext gameContext, int companyId)
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
