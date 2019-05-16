using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static int GetClientLoyalty(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            int app = GetAppLoyaltyBonus(gameContext, companyId);
            int pricing = GetClientLoyaltyPricingPenalty(gameContext, companyId);
            int marketRequirement = GetClientLoyaltyMarketSituationBonus(gameContext, companyId);

            return app - bugs - pricing - marketRequirement;
        }

        public static string GetClientLoyaltyDescription(GameContext gameContext, int companyId, UserType userType)
        {
            BonusContainer bonusContainer = new BonusContainer("Client loyalty is", GetClientLoyalty(gameContext, companyId, userType));

            bonusContainer.Append("App level", GetAppLoyaltyBonus(gameContext, companyId));
            bonusContainer.Append("Market demand", -GetClientLoyaltyMarketSituationBonus(gameContext, companyId));
            bonusContainer.Append("Bugs", -GetClientLoyaltyBugPenalty(gameContext, companyId));
            bonusContainer.Append("Pricing", -GetClientLoyaltyPricingPenalty(gameContext, companyId));

            return bonusContainer.ToString();
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
