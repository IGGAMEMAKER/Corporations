namespace Assets.Utils
{
    public static partial class Economy
    {
        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            if (Companies.IsProductCompany(c))
                return GetProductCompanyMaintenance(c, gameContext);
            else
                return GetGroupMaintenance(gameContext, c.company.Id);
        }

        internal static long GetCompanyMaintenance(GameContext gameContext, int companyId)
        {
            var c = Companies.GetCompany(gameContext, companyId);

            return GetCompanyMaintenance(c, gameContext);
        }
    }
}
