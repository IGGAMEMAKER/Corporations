﻿using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.ClientList.Values.Sum();
        }

        public static long GetClients(GameEntity company, int segmentId)
        {
            return company.marketing.ClientList.ContainsKey(segmentId) ? company.marketing.ClientList[segmentId] : 0;
        }

        public static void AddClients(GameEntity company, long clients, int segmentId)
        {
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

        public static long GetChurnClients(GameEntity product, int segmentId)
        {
            var churn = GetChurnRate(product, segmentId);

            var clients = GetClients(product, segmentId);

            return clients * churn / 100;
        }

        public static void ReleaseApp(GameContext gameContext, GameEntity product)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, C.RELEASE_BRAND_POWER_GAIN);
                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow, product.productPositioning.Positioning);

                product.isRelease = true;
                Investments.CompleteGoal(product, gameContext);
            }
        }
    }
}
