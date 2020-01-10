namespace Assets.Core
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

        public static void LooseClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            var newClients = marketing.clients - clients;
            if (newClients < 0)
                newClients = 0;

            company.ReplaceMarketing(newClients);
        }

        public static long GetChurnClients(GameContext gameContext, int companyId)
        {
            var c = Companies.GetCompany(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId);

            var clients = GetClients(c);

            var period = Constants.PERIOD;

            return clients * churn * period / 30 / 100;
        }

        public static void ReleaseApp(int companyId, GameContext gameContext)
        {
            var product = Companies.GetCompany(gameContext, companyId);

            ReleaseApp(product, gameContext);
        }
        public static void ReleaseApp(GameEntity product, GameContext gameContext)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, Constants.RELEASE_BRAND_POWER_GAIN);
                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow);

                product.isRelease = true;

                Products.SetMarketingFinancing(product, 1);
            }
        }
    }
}
