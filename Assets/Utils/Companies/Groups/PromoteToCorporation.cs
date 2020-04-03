namespace Assets.Core
{
    partial class Companies
    {
        public static bool IsPromotableToCorporation(GameEntity company)
        {
            var companyType = company.company.CompanyType;
            var isProperCompanyType = companyType == CompanyType.Group || companyType == CompanyType.Holding;

            return company.isManagingCompany && company.isIndependentCompany && isProperCompanyType;
        }

        public static void PromoteToCorporation(GameEntity company, GameContext gameContext)
        {
            if (!IsPromotableToCorporation(company)) return;

            company.ReplaceCompany(company.company.Id, company.company.Name, CompanyType.Corporation);

            NotificationUtils.NotifyAboutCorporationSpawn(gameContext, company.company.Id);
        }
    }
}
