namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }

        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(e, context);
        }

        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long cost;
            if (CompanyUtils.IsProductCompany(c))
                cost = GetProductCompanyCost(context, companyId);
            else
                cost = GetGroupOfCompaniesCost(context, c);

            //Debug.Log($"Get CompanyCost of {c.company.Name} = {cost}");

            long capital = c.companyResource.Resources.money;

            return cost + capital;
        }

        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = CompanyUtils.GetCompanyById(context, companyId);

            var desireToSell = CompanyUtils.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }

        public static long GetTeamMaintenance(GameContext gameContext, int companyId)
        {
            return GetTeamMaintenance(
                CompanyUtils.GetCompanyById(gameContext, companyId)
                );
        }

        public static int GetManagersMaintenance(GameEntity e)
        {
            return TeamUtils.GetManagers(e) * Constants.SALARIES_MANAGER;
        }

        public static int GetMarketersMaintenance(GameEntity e)
        {
            return TeamUtils.GetMarketers(e) * Constants.SALARIES_MARKETER;
        }

        public static int GetUniversalsMaintenance(GameEntity e)
        {
            return TeamUtils.GetUniversals(e) * Constants.SALARIES_UNIVERSAL;
        }

        public static int GetProgrammersMaintenance(GameEntity e)
        {
            return TeamUtils.GetProgrammers(e) * Constants.SALARIES_PROGRAMMER;
        }

        public static int GetCEOMaintenance(GameEntity e)
        {
            return TeamUtils.CountSpecialists(e, WorkerRole.Business) * Constants.SALARIES_CEO;
        }

        public static int GetTopManagersMaintenance(GameEntity e)
        {
            var directors = (
                TeamUtils.CountSpecialists(e, WorkerRole.MarketingDirector) +
                TeamUtils.CountSpecialists(e, WorkerRole.TechDirector)
            ) * Constants.SALARIES_DIRECTOR;

            var midManagers = (
                TeamUtils.CountSpecialists(e, WorkerRole.ProjectManager) +
                TeamUtils.CountSpecialists(e, WorkerRole.ProductManager)
                ) * Constants.SALARIES_PRODUCT_PROJECT_MANAGER;

            return directors + midManagers;
        }


        public static long GetTeamMaintenance(GameEntity e)
        {
            return
                GetCEOMaintenance(e) +
                GetUniversalsMaintenance(e) +
                GetManagersMaintenance(e) +
                GetMarketersMaintenance(e) +
                GetProgrammersMaintenance(e) +
                GetTopManagersMaintenance(e);
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyBaseCost(context, companyId);

            return GetCompanyCost(context, companyId);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }

        public static bool IsCompanyProfitable(GameContext gameContext, int companyId)
        {
            return GetBalanceChange(gameContext, companyId) > 0;
        }

        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyMaintenance(c, gameContext);
            else
                return GetGroupMaintenance(gameContext, c.company.Id);
        }

        internal static long GetCompanyMaintenance(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetCompanyMaintenance(c, gameContext);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static long GetBalanceChange(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetBalanceChange(c, context);
        }

        internal static bool IsProfitable(GameContext gameContext, int companyId)
        {
            return GetBalanceChange(gameContext, companyId) > 0;
        }

        internal static bool IsROICounable(GameEntity c, GameContext context)
        {
            return GetCompanyMaintenance(c, context) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }


        public static long GetOptimalProductCompanyMaintenance(GameContext gameContext, GameEntity product)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var nicheTeamMaintenance = NicheUtils.GetTeamMaintenanceCost(niche);
            var marketingMaintenance = NicheUtils.GetBaseMarketingMaintenance(niche).money;

            return nicheTeamMaintenance + marketingMaintenance;
        }

        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            var income = GetCompanyIncome(product, gameContext);
            var maintenance = GetOptimalProductCompanyMaintenance(gameContext, product);

            return maintenance > income;
        }
    }
}
