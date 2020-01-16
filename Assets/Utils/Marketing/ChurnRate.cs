using System.Linq;

namespace Assets.Core
{
    public static partial class MarketingUtils
    {
        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static GameEntity[] GetDumpingCompetitors(GameContext gameContext, GameEntity product) => GetDumpingCompetitors(gameContext, Markets.GetNiche(gameContext, product), product);
        public static GameEntity[] GetDumpingCompetitors(GameContext gameContext, GameEntity niche, GameEntity product)
        {
            var competitors = Markets.GetProductsOnMarket(niche, gameContext);

            var dumpingCompetitors = competitors.Where(p => p.isDumping && p.company.Id != product.company.Id);

            return dumpingCompetitors.ToArray();
        }

        public static bool HasDumpingCompetitors(GameContext gameContext, GameEntity product)
        {
            return false;
            return GetDumpingCompetitors(gameContext, product).Count() > 0;
        }

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId)
        {
            var c = Companies.GetCompany(gameContext, companyId);
            var state = Markets.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = Products.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == MarketState.Death;


            var niche = Markets.GetNiche(gameContext, c.product.Niche);
            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
            var baseValue = GetChurnRateBasedOnMonetisationType(monetisation);

            var isCompetitorDumping = HasDumpingCompetitors(gameContext, c);

            var retentionImprovement = c.features.features[ProductFeature.Retention];

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base value", baseValue)
                .Append("Retention features", -retentionImprovement * 2)
                .AppendAndHideIfZero("DUMPING", c.isDumping ? -100 : 0)
                .AppendAndHideIfZero("Competitor is DUMPING", isCompetitorDumping ? 15 : 0)
                .Append($"Concept difference to market ({fromProductLevel})", fromProductLevel * fromProductLevel)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(1, 100)
                ;
        }

        public static int GetChurnRateBasedOnMonetisationType(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.Adverts:
                    return 70;

                case Monetisation.Service:
                    return 40;

                case Monetisation.Enterprise:
                case Monetisation.IrregularPaid:
                case Monetisation.Paid:
                default:
                    return 20;
            }
        }
    }
}
