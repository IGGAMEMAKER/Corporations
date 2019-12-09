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
            var c = CompanyUtils.GetCompany(gameContext, companyId);

            return GetCompanyMaintenance(c, gameContext);
        }
    }
}
