using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long GetCompanyMaintenance(GameContext gameContext, int companyId) => GetCompanyMaintenance(gameContext, Companies.Get(gameContext, companyId));
        public static long GetCompanyMaintenance(GameContext gameContext, GameEntity c)
        {
            if (Companies.IsProductCompany(c))
                return GetProductCompanyMaintenance(c, gameContext);
            else
                return GetGroupMaintenance(gameContext, c);
        }

        private static long GetGroupMaintenance(GameContext gameContext, GameEntity company)
        {
            var holdings = Investments.GetHoldings(gameContext, company, true);

            return holdings
                .Sum(h => h.control * GetCompanyMaintenance(gameContext, h.companyId) / 100);
        }
    }
}
