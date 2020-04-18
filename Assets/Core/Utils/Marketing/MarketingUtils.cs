namespace Assets.Core
{
    public static partial class Marketing
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

        public static void LoseClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            var newClients = marketing.clients - clients;
            if (newClients < 0)
                newClients = 0;

            company.ReplaceMarketing(newClients);
        }

        public static long GetChurnClients(GameContext gameContext, int companyId)
        {
            var c = Companies.Get(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId);

            var clients = GetClients(c);

            //var period = Constants.PERIOD;

            return clients * churn / 100;
        }

        public static void ReleaseApp(GameContext gameContext, int companyId) => ReleaseApp(gameContext, Companies.Get(gameContext, companyId));
        public static void ReleaseApp(GameContext gameContext, GameEntity product)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, Balance.RELEASE_BRAND_POWER_GAIN);
                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow);

                product.isRelease = true;
                Investments.CompleteGoal(product, gameContext);
            }
        }
    }
}
