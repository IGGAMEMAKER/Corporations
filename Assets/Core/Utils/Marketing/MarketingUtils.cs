using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetUsers(GameEntity company)
        {
            return company.marketing.ClientList.Values.Sum();
        }

        public static void AddClients(GameEntity company, long clients)
        {
            int segmentId = 0;
            var marketing = company.marketing;

            if (!marketing.ClientList.ContainsKey(segmentId))
                marketing.ClientList[segmentId] = 0;

            marketing.ClientList[segmentId] += clients;

            if (marketing.ClientList[segmentId] < 0)
            {
                marketing.ClientList[segmentId] = 0;
            }

            company.ReplaceMarketing(marketing.ClientList);
        }

        public static long GetChurnClients(GameEntity product, GameContext gameContext)
        {
            var churn = GetChurnRate(product, gameContext);

            var clients = (double)GetUsers(product);

            return (long)(clients * churn / 100);
        }

        public static void ReleaseApp(GameContext gameContext, GameEntity product)
        {
            if (!product.isRelease)
            {
                //AddBrandPower(product, C.RELEASE_BRAND_POWER_GAIN);

                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow);

                product.isRelease = true;

                Companies.LogSuccess(product, "RELEASE");
            }
        }
    }
}
