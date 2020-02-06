namespace Assets.Core
{
    partial class Companies
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var product = Get(context, companyId);

            if (!product.isIndependentCompany)
                return -1;

            product.isIndependentCompany = false;



            var name = product.company.Name;

            var futureName = Enums.GetShortNicheName(product.product.Niche);
            int companyGroupId = GenerateCompanyGroup(context, futureName + " Group", companyId).company.Id;

            AttachToGroup(context, companyGroupId, companyId);


            var groupCo = Get(context, companyGroupId);

            var niche = product.product.Niche;
            var industry = Markets.GetIndustry(niche, context);
            AddFocusIndustry(industry, groupCo);

            AddFocusNiche(niche, groupCo, context);
            groupCo.isManagingCompany = true;

            // manage partnerships of product company
            foreach (var p in GetPartnershipCopy(product))
            {
                var acceptor = Get(context, p);

                AcceptStrategicPartnership(groupCo, acceptor);
            }
            RemoveAllPartnerships(product, context);



            NotifyAboutCompanyPromotion(context, companyGroupId, name);

            return companyGroupId;
        }

        public static bool IsProductWantsToGrow(GameEntity product, GameContext gameContext)
        {
            if (!product.isIndependentCompany)
                return false;


            var ambitions = Humans.GetFounderAmbition(gameContext, product.cEO.HumanId);
            return ambitions == Ambition.RuleCorporation;
        }

        public static void NotifyAboutCompanyPromotion(GameContext gameContext, int companyId, string previousName)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyTypeChange(companyId, previousName));
        }
    }
}
