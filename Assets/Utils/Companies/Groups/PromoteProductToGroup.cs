namespace Assets.Utils
{
    partial class Companies
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var product = GetCompany(context, companyId);

            if (!product.isIndependentCompany)
                return -1;

            product.isIndependentCompany = false;

            var name = product.company.Name;

            int companyGroupId = GenerateCompanyGroup(context, name + " Group", companyId).company.Id;

            AttachToGroup(context, companyGroupId, companyId);


            var groupCo = GetCompany(context, companyGroupId);

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
    }
}
