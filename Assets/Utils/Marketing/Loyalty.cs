using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        static BonusContainer GetClientLoyaltyBonus(GameContext gameContext, int companyId, UserType userType)
        {
            int app = GetClientLoyaltyAppPart(gameContext, companyId);
            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            int pricing = GetClientLoyaltyPricingPenalty(gameContext, companyId);
            int marketRequirement = GetClientLoyaltyMarketRequirementsPenalty(gameContext, companyId);

            bool isOnlyPlayer = NicheUtils.GetPlayersOnMarket(gameContext, companyId).Count() == 1;
            int onlyPlayerBonus = isOnlyPlayer ? 35 : 0;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            int SegmentFocus = c.targetUserType.UserType == userType ? 15 : 0;

            return new BonusContainer("Client loyalty is")
                .Append("Product level", app)
                .Append("Market requirements", -marketRequirement)
                .AppendAndHideIfZero("We are focusing on them", SegmentFocus)
                .AppendAndHideIfZero("Is only company", onlyPlayerBonus)
                .AppendAndHideIfZero("Bugs", -bugs)
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

        public static int GetClientLoyaltyAppPart(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return c.product.ProductLevel * 5;
        }

        public static int GetClientLoyaltyMarketRequirementsPenalty(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return NicheUtils.GetMarketDemand(gameContext, c.product.Niche) * 4;
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
