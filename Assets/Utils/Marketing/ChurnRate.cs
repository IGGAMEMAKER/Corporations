namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var state = NicheUtils.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == NicheLifecyclePhase.Death;


            var improvements = c.productImprovements.Improvements[ProductImprovement.Retention];
            var improvementModifier = improvements;


            var niche = NicheUtils.GetNicheEntity(gameContext, c.product.Niche);
            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
            var baseValue = GetChurnRateBasedOnMonetisationType(monetisation);

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base for " + monetisation.ToString() + " products", baseValue)
                .Append($"Concept difference to market ({fromProductLevel})", fromProductLevel * fromProductLevel)
                .AppendAndHideIfZero("Retention Improvements", -improvements)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(2, 100);
        }

        public static int GetChurnRateBasedOnMonetisationType(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.Enterprise:
                case Monetisation.IrregularPaid:
                case Monetisation.Paid:
                    return 2;

                case Monetisation.Adverts:
                    return 7;

                case Monetisation.Service:
                    return 4;

                default:
                    return 2;
            }
        }
    }
}
