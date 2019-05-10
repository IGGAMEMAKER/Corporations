namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static string GetInvestmentAttractivenessDescription(GameContext gameContext, int id)
        {
            return "Because";
        }

        public static bool IsMeetsIPORequirements(GameContext gameContext, int companyId)
        {
            var c = GetCompanyById(gameContext, companyId);

            bool meetsCostRequirement = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_COST;
            bool meetsProfitRequirement = CompanyEconomyUtils.GetBalanceChange(gameContext, companyId) > Constants.IPO_REQUIREMENTS_COMPANY_PROFIT;
            bool meetsShareholderRequirement = c.shareholders.Shareholders.Count >= 3;

            return meetsCostRequirement && meetsProfitRequirement && meetsShareholderRequirement;
        }

        static int RestrictValue(int value, int min, int max)
        {
            if (value > max)
                return max;
            if (value < min)
                return min;

            return value;
        }

        static int ProjectRange(int value, int min, int max, int newMin, int newMax)
        {
            int input = RestrictValue(value, min, max);

            int percent = (input - min) / (max - min);

            int output = percent * (newMax - newMin) + newMin;

            return output;
        }

        public static int GetInvestmentAttractiveness(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (IsProductCompany(c))
            {
                //UnityEngine.Random.Range(0, 3)
                int risk = NicheUtils.GetCompanyRisk(context, companyId);

                return ProjectRange(100 - risk, 0, 100, 0, 10);
            } else
            {
                int ROI = System.Convert.ToInt32(CompanyEconomyUtils.GetBalanceROI(c, context));

                if (ROI < -50)
                    return 0;

                if (ROI < 0)
                    return 1;

                if (ROI < 20)
                    return ROI / 4;

                if (ROI < 50)
                    return 5;

                return 7;
            }
        }
    }
}
