namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static bool IsPromotableToCorporation(GameEntity company)
        {
            var companyType = company.company.CompanyType;
            var isProperCompanyType = companyType == CompanyType.Group || companyType == CompanyType.Holding;

            return company.isManagingCompany && company.isIndependentCompany && isProperCompanyType;
        }

        internal static void PromoteToCorporation(GameEntity company, GameContext gameContext)
        {
            if (!IsPromotableToCorporation(company)) return;

            company.ReplaceCompany(company.company.Id, company.company.Name, CompanyType.Corporation);

            NotifyAboutCorporationSpawn(gameContext, company.company.Id);
        }

        public static void NotifyAboutCorporationSpawn(GameContext gameContext, int companyId)
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageCorporationSpawn(companyId));
        }
    }
}
