using System.Linq;

namespace Assets.Core
{
    partial class Economy
    {
        private static long GetProductCost(GameEntity company)
        {
            //var risks = Markets.GetCompanyRisk(context, company);

            return GetProductCompanyBaseCost(company); // * (100 - risks) / 100;
        }

        public static long GetProductPotential(GameEntity company)
        {
            // judge by potential
            var segmentId = Marketing.GetCoreAudienceId(company);
            var segment = Marketing.GetAudienceInfos()[segmentId];

            var income = GetBaseIncomeByMonetizationType(company);

            var incomeMultiplier = income * segment.Bonuses.Where(b => b.isMonetisationFeature).Select(b => b.Max).Sum();



            long baseCost = 1_000_000;

            return (long)((double)baseCost * (100f + incomeMultiplier) / 100f);

            var max = segment.Size;
            var possiblePortion = 5;
            var potentialBaseIncome = income * (100f + incomeMultiplier) / 100f;
            return GetCompanyIncomeBasedCost((long)(potentialBaseIncome * (double)max) * possiblePortion / 1000);
        }

        public static long GetProductCompanyBaseCost(GameEntity company)
        {
            var potential = GetProductPotential(company);

            if (company.isRelease)
            {
                long audienceCost = GetClientBaseCost(company);
                long profitCost = GetProductIncome(company) * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;

                return audienceCost + profitCost + potential;
            }
            else
            {
                return potential;
            }
        }

        public static long GetClientBaseCost(GameEntity c)
        {
            var segments = Marketing.GetAudienceInfos();

            //segments.Sum(s => Economy.GetIncomePerSegment(c, s.ID) * 

            return 0;
            //return Marketing.GetClients(c) * 100;
        }
    }
}
