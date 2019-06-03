using Assets.Classes;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void RemovePlayerControlledCompany(GameContext context, int id)
        {
            GetCompanyById(context, id).isControlledByPlayer = false;
        }

        internal static void BecomeCEO(GameContext gameContext, int companyID)
        {
            SetPlayerControlledCompany(gameContext, companyID);
        }

        public static void SetCEO(GameContext context, int companyId, int humanId)
        {

        }

        public static void SetPlayerControlledCompany(GameContext context, int id)
        {
            GetCompanyById(context, id).isControlledByPlayer = true;
        }

        // it is done for player only!
        internal static void LeaveCEOChair(GameContext gameContext, int companyId)
        {
            RemovePlayerControlledCompany(gameContext, companyId);

            // update company ceo component
        }
    }

    public static partial class CompanyUtils
    {
        public static void SpendResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Spend(resource);

            //company.ReplaceCompanyResource()
        }
    }
}
