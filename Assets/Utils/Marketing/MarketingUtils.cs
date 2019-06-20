using Assets.Classes;
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

        public static long GetClients(GameEntity company)
        {
            long amount = 0;

            foreach (var p in company.marketing.Segments)
                amount += p.Value;

            return amount;
        }

        public static void AddClients(GameEntity company, UserType userType, long clients)
        {
            var marketing = company.marketing;

            marketing.Segments[UserType.Core] += clients;

            company.ReplaceMarketing(marketing.BrandPower, marketing.Segments);
        }

        public static NicheCostsComponent GetNicheCosts(GameContext gameContext, GameEntity company)
        {
            return GetNicheCosts(gameContext, company.product.Niche);
        }

        public static NicheCostsComponent GetNicheCosts(GameContext gameContext, NicheType nicheType)
        {
            return NicheUtils.GetNicheEntity(gameContext, nicheType).nicheCosts;
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

            //if (userType == UserType.Regular)
            //{
            //    hint.AppendFormat("<color={0}>Also, {2} clients will be promoted to {1}</color>",
            //        VisualConstants.COLOR_POSITIVE,
            //        EnumUtils.GetFormattedUserType(UserType.Core),
            //        promoted
            //        );
            //}

            hint.AppendLine();

            return hint.ToString();
        }

        public static TeamResource GetReleaseCost()
        {
            return new TeamResource(0, 0, 50, 0, 1000);
        }

        public static int GetReleaseBrandPowerGain()
        {
            return 20;
        }

        public static void ReleaseApp(GameEntity product)
        {
            var need = GetReleaseCost();

            bool enoughResources = CompanyUtils.IsEnoughResources(product, need);

            if (!product.isRelease && enoughResources)
            {
                product.isRelease = true;
                product.ReplaceMarketing(product.marketing.BrandPower + GetReleaseBrandPowerGain(), product.marketing.Segments);

                CompanyUtils.SpendResources(product, need);
            }
        }
    }
}
