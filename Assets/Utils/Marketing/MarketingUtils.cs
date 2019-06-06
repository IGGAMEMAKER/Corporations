using Assets.Classes;
using Assets.Utils.Formatting;
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
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetMarketDiff(gameContext, c);
        }

        public static int GetMarketDiff(GameContext gameContext, GameEntity productCompany)
        {
            var best = NicheUtils.GetLeaderApp(gameContext, productCompany.company.Id);

            return best.product.ProductLevel - productCompany.product.ProductLevel;
        }

        public static long GetClients(GameEntity company)
        {
            long amount = 0;

            foreach (var p in company.marketing.Segments)
                amount += p.Value;

            return amount;
        }

        public static string GetAudienceHint(GameContext gameContext, UserType userType, GameEntity company)
        {
            StringBuilder hint = new StringBuilder();

            var id = company.company.Id;

            var churn = GetChurnRate(gameContext, id, userType);
            var churnClients = GetChurnClients(gameContext, id, userType);
            var promoted = GetPromotionClients(gameContext, id, userType);

            hint.AppendFormat("Due to our churn rate ({0}%)", churn);
            hint.AppendFormat(" we lose {0} clients each month\n", Visuals.Negative(churnClients.ToString()));

            if (userType == UserType.Regular)
            {
                hint.AppendFormat("<color={0}>Also, {2} clients will be promoted to {1}</color>",
                    VisualConstants.COLOR_POSITIVE,
                    EnumUtils.GetFormattedUserType(UserType.Core),
                    promoted
                    );
            }

            hint.AppendLine();

            return hint.ToString();
        }

        public static void ReleaseApp(GameEntity product)
        {
            var need = new TeamResource(0, 0, 500, 0, 1000);

            bool enoughResources = true;

            if (!product.isRelease && enoughResources)
            {
                CompanyUtils.SpendResources(product, need);
                product.isRelease = true;
                product.ReplaceMarketing(product.marketing.BrandPower + 20, product.marketing.Segments);
            }
        }
    }
}
