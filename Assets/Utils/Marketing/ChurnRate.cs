using System.Linq;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static GameEntity[] GetDumpingCompetitors(GameContext gameContext, GameEntity product) => GetDumpingCompetitors(gameContext, NicheUtils.GetNiche(gameContext, product), product);
        public static GameEntity[] GetDumpingCompetitors(GameContext gameContext, GameEntity niche, GameEntity product)
        {
            var competitors = NicheUtils.GetProductsOnMarket(niche, gameContext);

            var dumpingCompetitors = competitors.Where(p => p.isDumping && p.company.Id != product.company.Id);

            return dumpingCompetitors.ToArray();
        }

        public static bool HasDumpingCompetitors(GameContext gameContext, GameEntity product)
        {
            return GetDumpingCompetitors(gameContext, product).Count() > 0;
        }

        public static Bonus GetChurnBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var state = NicheUtils.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == NicheLifecyclePhase.Death;


            var niche = NicheUtils.GetNiche(gameContext, c.product.Niche);
            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
            var baseValue = GetChurnRateBasedOnMonetisationType(monetisation);

            var isCompetitorDumping = HasDumpingCompetitors(gameContext, c);

            return new Bonus("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base value", baseValue)
                .AppendAndHideIfZero("DUMPING", c.isDumping ? -100 : 0)
                .AppendAndHideIfZero("Competitor is DUMPING", isCompetitorDumping ? 15 : 0)
                .Append($"Concept difference to market ({fromProductLevel})", fromProductLevel * fromProductLevel)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(0, 100)
                ;
        }

        public static int GetChurnRateBasedOnMonetisationType(Monetisation monetisation)
        {
            return 2;
            switch (monetisation)
            {
                case Monetisation.Adverts:
                    return 7;

                case Monetisation.Service:
                    return 4;

                case Monetisation.Enterprise:
                case Monetisation.IrregularPaid:
                case Monetisation.Paid:
                default:
                    return 2;
            }
        }
    }
}
