namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (!c.isIndependentCompany)
                return -1;

            var niche = c.product.Niche;

            var industry = NicheUtils.GetIndustry(niche, context);

            var name = c.company.Name;

            int companyGroupId = GenerateCompanyGroup(context, name + " Group", companyId).company.Id;

            AttachToGroup(context, companyGroupId, companyId);
            c.isIndependentCompany = false;

            var groupCo = GetCompanyById(context, companyGroupId);
            AddFocusIndustry(industry, groupCo);
            AddFocusNiche(niche, groupCo, context);
            groupCo.isManagingCompany = true;

            NotifyAboutCompanyPromotion(context, companyGroupId, name);

            return companyGroupId;
        }

        public static void NotifyAboutCompanyPromotion(GameContext gameContext, int companyId, string previousName)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyTypeChange(companyId, previousName));
        }
    }
}
