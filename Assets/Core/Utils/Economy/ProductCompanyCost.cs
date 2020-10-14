using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = Markets.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameContext context, int companyId) => GetProductCompanyBaseCost(context, Companies.Get(context, companyId));
        public static long GetProductCompanyBaseCost(GameContext context, GameEntity company)
        {
            if (company.isRelease)
            {
                long audienceCost = GetClientBaseCost(company);
                long profitCost = GetCompanyIncomeBasedCost(context, company);

                return audienceCost + profitCost;
            }
            else
            {
                // judge by potential
                var segmentId = Marketing.GetCoreAudienceId(company, context);
                try
                {
                    var info = Marketing.GetAudienceInfos();
                    var segment = info[segmentId];

                    var max = segment.Size;
                    var income = GetBaseIncomeByMonetisationType(company);

                    var incomeMultiplier = income * segment.Bonuses.Where(b => b.isMonetisationFeature).Select(b => b.Max).Sum();

                    var potentialBaseIncome = income * (100f + incomeMultiplier) / 100f;

                    var possiblePortion = 5;

                    long baseCost = 1000000;

                    return (long)((double)baseCost * (100f + incomeMultiplier) / 100f);
                    return GetCompanyIncomeBasedCost((long)(potentialBaseIncome * (double)max) * possiblePortion / 1000);
                }
                catch
                {
                    Debug.LogError("Calculating potential of " + company.company.Name + " by segment " + segmentId);
                    return 0;
                }
            }
        }

        public static long GetClientBaseCost(GameEntity c)
        {
            return 0;
            //return Marketing.GetClients(c) * 100;
        }
    }
}
