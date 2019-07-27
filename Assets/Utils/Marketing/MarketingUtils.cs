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
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, UserType userType, long clients)
        {
            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.clients + clients);
        }

        public static long GetCompanyClientBatch(GameContext gameContext, GameEntity company)
        {
            return GetCurrentClientFlow(gameContext, company.product.Niche);
        }

        public static long GetCurrentClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            var modifier = niche.nicheState.Growth[niche.nicheState.Phase];

            var costs = GetNicheCosts(gameContext, nicheType);

            return costs.ClientBatch * modifier;
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

            hint.AppendFormat("Due to our churn rate ({0}%)", churn);
            hint.AppendFormat(" we lose {0} clients each month\n", Visuals.Negative(churnClients.ToString()));

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
                AddBrandPower(product, GetReleaseBrandPowerGain());

                CompanyUtils.SpendResources(product, need);
            }
        }
    }
}
