using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static class ProductEconomicsUtils
    {
        public static long GetIncome(GameEntity e)
        {
            int basePayments = 15;
            int pricing = 1;
            int price = basePayments * pricing;
            long payments = price;

            return e.marketing.Clients * payments;
        }

        internal static long GetBalance(GameEntity e)
        {
            return GetIncome(e) - GetTeamMaintenance(e);
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            return (e.team.Managers + e.team.Marketers + e.team.Programmers) * 2000;
        }
    }
}
