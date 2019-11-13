namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var state = NicheUtils.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == NicheLifecyclePhase.Death;


            var improvements = c.productImprovements.Improvements[ProductImprovement.Retention];
            var improvementModifier = improvements;

            var baseValue = 2;

            var niche = NicheUtils.GetNicheEntity(gameContext, c.product.Niche);
            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;

            switch (monetisation)
            {
                case Monetisation.Enterprise:
                case Monetisation.IrregularPaid:
                case Monetisation.Paid:
                    baseValue = 2;
                    break;

                case Monetisation.Adverts:
                    baseValue = 7;
                    break;

                case Monetisation.Service:
                    baseValue = 4;
                    break;
            }

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base for " + monetisation.ToString() + " products", baseValue)
                .Append($"Concept difference to market ({fromProductLevel})", fromProductLevel * fromProductLevel)
                .AppendAndHideIfZero("Retention Improvements", -improvements)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(2, 100);
        }

        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static long GetChurnClients(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId);

            var clients = GetClients(c);

            var period = EconomyUtils.GetPeriodDuration();

            return clients * churn * period / 30 / 100;
        }
    }
}
