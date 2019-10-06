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
            return 0;
        }
    }
}
