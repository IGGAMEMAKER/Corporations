using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static int GetClientLoyalty(GameContext gameContext, int companyId, UserType userType)
        {
            return UnityEngine.Random.Range(-5, 25);
        }

        public static long GetClients(GameEntity company)
        {
            long amount = 0;

            foreach (var p in company.marketing.Segments)
                amount += p.Value;

            return amount;
        }
    }
}
