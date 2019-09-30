namespace Assets.Utils
{
    public static partial class EconomyUtils
    {
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


        public static long GetOptimalProductCompanyMaintenance(GameContext gameContext, GameEntity product)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var nicheTeamMaintenance = NicheUtils.GetTeamMaintenanceCost(niche);
            var marketingMaintenance = NicheUtils.GetBaseMarketingMaintenance(niche).money;

            return nicheTeamMaintenance + marketingMaintenance;
        }

        public static long GetTeamMaintenance(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            return GetTeamMaintenance(c);
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

        public static long GetPromotedTeamMaintenance(GameEntity product)
        {
            var newStatus = TeamUtils.GetNextTeamSize(product.team.TeamStatus);

            return GetAbstractTeamMaintenance(newStatus);
        }

        public static long GetAbstractTeamMaintenance(TeamStatus teamStatus)
        {
            switch (teamStatus)
            {
                case TeamStatus.Solo: return 0;
                case TeamStatus.Pair: return 10000;
                case TeamStatus.SmallTeam: return 50000;
                case TeamStatus.BigTeam: return 300000;
                case TeamStatus.Department: return 2300000;

                default: return -100000;
            }
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            return GetAbstractTeamMaintenance(e.team.TeamStatus);

            return
                GetCEOMaintenance(e) +
                GetUniversalsMaintenance(e) +
                GetManagersMaintenance(e) +
                GetMarketersMaintenance(e) +
                GetProgrammersMaintenance(e) +
                GetTopManagersMaintenance(e);
        }
    }
}
