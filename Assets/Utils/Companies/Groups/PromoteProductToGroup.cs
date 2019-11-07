namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var product = GetCompanyById(context, companyId);

            if (!product.isIndependentCompany)
                return -1;

            product.isIndependentCompany = false;

            var name = product.company.Name;

            int companyGroupId = GenerateCompanyGroup(context, name + " Group", companyId).company.Id;

            AttachToGroup(context, companyGroupId, companyId);


            var groupCo = GetCompanyById(context, companyGroupId);

            var niche = product.product.Niche;
            var industry = NicheUtils.GetIndustry(niche, context);
            AddFocusIndustry(industry, groupCo);

            AddFocusNiche(niche, groupCo, context);
            groupCo.isManagingCompany = true;

            NotifyAboutCompanyPromotion(context, companyGroupId, name);

            return companyGroupId;
        }

        public static bool IsProductWantsToGrow(GameEntity product, GameContext gameContext)
        {
            if (!product.isIndependentCompany)
                return false;


            var ambitions = HumanUtils.GetFounderAmbition(gameContext, product.cEO.HumanId);
            return ambitions == Ambition.RuleCorporation;
        }

        public static void NotifyAboutCompanyPromotion(GameContext gameContext, int companyId, string previousName)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyTypeChange(companyId, previousName));
        }



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
