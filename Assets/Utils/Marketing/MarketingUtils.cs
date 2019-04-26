using Assets.Utils.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return app + improvements - bugs;
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

        public static string GetClientLoyaltyDescription(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            int loyalty = GetClientLoyalty(gameContext, companyId, userType);
            int app = GetSegmentImprovementsBonus(gameContext, companyId, userType);
            int improvements = GetSegmentImprovementsBonus(gameContext, companyId, userType);

            int bugs = GetClientLoyaltyBugPenalty(gameContext, companyId);
            
            // market situation?

            return $"Current loyalty is {loyalty} due to:\n\nApp level: +{app}\n{EnumUtils.GetFormattedUserType(userType)} improvements: +{improvements}\n\nBugs: -{bugs}";
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
