namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsMeetsIPOCompanyCostRequirement(GameContext gameContext, int companyId)
        {
            var company = Get(gameContext, companyId);

            return Economy.CostOf(company, gameContext) > C.IPO_REQUIREMENTS_COMPANY_COST;
        }

        public static bool IsMeetsIPOProfitRequirement(GameContext gameContext, int companyId)
        {
            var company = Get(gameContext, companyId);
            return Economy.GetProfit(gameContext, company) > C.IPO_REQUIREMENTS_COMPANY_PROFIT;
        }

        public static bool IsMeetsIPOShareholderRequirement(GameContext gameContext, int companyId)
        {
            var c = Get(gameContext, companyId);

            return c.shareholders.Shareholders.Count >= 3;
        }

        public static bool IsMeetsIPORequirements(GameContext gameContext, int companyId)
        {
            bool meetsCostRequirement = IsMeetsIPOCompanyCostRequirement(gameContext, companyId);
            bool meetsProfitRequirement = IsMeetsIPOProfitRequirement(gameContext, companyId);
            bool meetsShareholderRequirement = IsMeetsIPOShareholderRequirement(gameContext, companyId);

            return meetsCostRequirement && meetsProfitRequirement && meetsShareholderRequirement;
        }

        public static bool IsCanGoPublic(GameContext gameContext, int companyId)
        {
            bool isAlreadyPublic = Get(gameContext, companyId).isPublicCompany;
            bool meetsIPORequirements = IsMeetsIPORequirements(gameContext, companyId);

            return !isAlreadyPublic && meetsIPORequirements;
        }
    }
}
