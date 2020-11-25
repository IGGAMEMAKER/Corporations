namespace Assets.Core
{
    partial class Companies
    {
        public static void SetIndependence(GameEntity company, bool independence)
        {
            if (independence)
            {
                company.isIndependentCompany = true;

                if (!company.hasInvestmentStrategy)
                    company.AddInvestmentStrategy(CompanyGrowthStyle.Aggressive, VotingStyle.Percent75, RandomEnum<InvestorInterest>.GenerateValue(InvestorInterest.None));
            }
            else
            {
                company.isIndependentCompany = false;

                if (company.hasInvestmentStrategy)
                    company.RemoveInvestmentStrategy();
            }
        }

        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var product = Get(context, companyId);

            if (!product.isIndependentCompany)
                return -1;

            SetIndependence(product, false);



            var name = product.company.Name;

            var futureName = Enums.GetShortNicheName(product.product.Niche);
            var group = GenerateCompanyGroup(context, futureName + " Group", companyId);
            int companyGroupId = group.company.Id;
            //int companyGroupId = GenerateCompanyGroup(context, futureName + " Group", companyId).company.Id;

            //AttachToGroup(context, companyGroupId, companyId);
            AttachToGroup(context, group, product);


            //var groupCo = Get(context, companyGroupId);

            var niche = product.product.Niche;
            var industry = Markets.GetIndustry(niche, context);
            AddFocusIndustry(industry, group);

            AddFocusNiche(group, niche, context);
            group.isManagingCompany = true;

            // manage partnerships of product company
            foreach (var p in GetPartnershipCopy(product))
            {
                var acceptor = Get(context, p);

                AcceptStrategicPartnership(group, acceptor);
            }
            RemoveAllPartnerships(product, context);



            NotifyAboutCompanyPromotion(context, companyGroupId, name);
            ScheduleUtils.TweakCampaignStats(context, CampaignStat.PromotedCompanies);

            return companyGroupId;
        }

        public static bool IsProductWantsToGrow(GameEntity product, GameContext gameContext)
        {
            if (!product.isIndependentCompany)
                return false;


            var ambitions = Humans.GetAmbition(gameContext, product.cEO.HumanId);
            return ambitions == Ambition.RuleCorporation;
        }

        public static void NotifyAboutCompanyPromotion(GameContext gameContext, int companyId, string previousName)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyTypeChange(companyId, previousName));
        }
    }
}
