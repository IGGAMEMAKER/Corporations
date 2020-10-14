using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Economy
    {
        private static long GetProductCost(GameEntity company)
        {
            //var risks = Markets.GetCompanyRisk(context, company);

            return GetProductCompanyBaseCost(company); // * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameEntity company)
        {
            if (company.isRelease)
            {
                long audienceCost = GetClientBaseCost(company);
                long profitCost = GetIncomeFromProduct(company) * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;

                return audienceCost + profitCost;
            }
            else
            {
                // judge by potential
                var segmentId = Marketing.GetCoreAudienceId(company);
                try
                {
                    var segment = Marketing.GetAudienceInfos()[segmentId];

                    var income = GetBaseIncomeByMonetisationType(company);

                    var incomeMultiplier = income * segment.Bonuses.Where(b => b.isMonetisationFeature).Select(b => b.Max).Sum();



                    long baseCost = 1_000_000;

                    return (long)((double)baseCost * (100f + incomeMultiplier) / 100f);

                    var max = segment.Size;
                    var possiblePortion = 5;
                    var potentialBaseIncome = income * (100f + incomeMultiplier) / 100f;
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
