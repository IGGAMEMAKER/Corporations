using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.ClientList.Values.Sum();
            return company.marketing.clients;
        }

        public static long GetClients(GameEntity company, int segmentId)
        {
            return company.marketing.ClientList.ContainsKey(segmentId) ? company.marketing.ClientList[segmentId] : 0;
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, long clients, int segmentId)
        {
            var marketing = company.marketing;

            if (!marketing.ClientList.ContainsKey(segmentId))
                marketing.ClientList[segmentId] = 0;

            marketing.ClientList[segmentId] += clients;

            company.ReplaceMarketing(marketing.clients + clients, marketing.ClientList);
            //company.ReplaceMarketing(marketing.clients + clients);
        }

        public static void LoseClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            var newClients = marketing.clients - clients;
            if (newClients < 0)
                newClients = 0;

            //company.ReplaceMarketing(newClients);
        }

        public static long GetChurnClients(GameContext gameContext, int companyId, int segmentId)
        {
            var c = Companies.Get(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId, segmentId);

            var clients = GetClients(c);

            //var period = Constants.PERIOD;

            return clients * churn / 100;
        }

        public static void ReleaseApp(GameContext gameContext, int companyId) => ReleaseApp(gameContext, Companies.Get(gameContext, companyId));
        public static void ReleaseApp(GameContext gameContext, GameEntity product)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, C.RELEASE_BRAND_POWER_GAIN);
                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow, product.productTargetAudience.SegmentId);

                product.isRelease = true;
                Investments.CompleteGoal(product, gameContext);
            }
        }
    }
}
