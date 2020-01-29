using Assets.Core.Formatting;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChurnRate(GameContext gameContext, int companyId) => GetChurnRate(gameContext, Companies.Get(gameContext, companyId));
        public static long GetChurnRate(GameContext gameContext, GameEntity company)
        {
            return GetChurnBonus(gameContext, company.company.Id).Sum();
        }

        public static float GetLifeTime(GameContext gameContext, int companyId)
        {
            var churn = GetChurnRate(gameContext, companyId);
            var oppositeChurn = (100 - churn) / 100f;

            return Mathf.Log(0.01f, oppositeChurn);
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

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId) => GetChurnBonus(gameContext, Companies.Get(gameContext, companyId));
        public static Bonus<long> GetChurnBonus(GameContext gameContext, GameEntity c)
        {
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
                .Append("Base value for " + Products.GetFormattedMonetisationType(monetisation), baseValue)
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
            //return 2;
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
