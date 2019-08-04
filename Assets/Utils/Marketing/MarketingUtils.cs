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

        public static void AddClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.clients + clients);
        }

        public static long GetCurrentClientFlow(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
            var phase = NicheUtils.GetMarketState(niche);

            var baseGrowthModifier = niche.nicheLifecycle.Growth[phase];

            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            return costs.ClientBatch * baseGrowthModifier;
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
