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

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base", 10)
                .Append("Concept", fromProductLevel)
                .Append("Retention Improvements", -improvements)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0);
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
