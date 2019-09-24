using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        static BonusContainer GetClientLoyaltyBonus(GameContext gameContext, int companyId)
        {
            var app = GetClientLoyaltyRelativeStrength(gameContext, companyId);

            bool isOnlyPlayer = NicheUtils.GetProductsOnMarket(gameContext, companyId).Count() == 1;
            int onlyPlayerBonus = isOnlyPlayer ? 30 : 0;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return new BonusContainer("Client loyalty is")
                .RenderTitle()
                .Append("Product Competitiveness", app)
                .AppendAndHideIfZero("Is only company", onlyPlayerBonus)
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
    }
}
