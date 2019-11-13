using Assets.Utils.Formatting;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
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
            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            var period = EconomyUtils.GetPeriodDuration();

            return costs.ClientBatch * period / 30;
        }

        public static void ReleaseApp(GameEntity product, GameContext gameContext)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, 20);
                var flow = GetCurrentClientFlow(gameContext, product.product.Niche);

                AddClients(product, 3 * flow);

                product.isRelease = true;
            }
        }
    }
}
