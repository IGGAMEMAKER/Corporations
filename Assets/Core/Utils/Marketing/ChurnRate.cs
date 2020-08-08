using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetChurnRate(GameContext gameContext, int companyId, int segmentId) => GetChurnRate(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static long GetChurnRate(GameContext gameContext, GameEntity company, int segmentId)
        {
            return GetChurnBonus(gameContext, company.company.Id, segmentId).Sum();
        }

        public static float GetLifeTime(GameContext gameContext, int companyId)
        {
            var c = Companies.Get(gameContext, companyId);
            var churn = GetChurnRate(gameContext, companyId, c.productTargetAudience.SegmentId);

            return 100 / churn;
            var oppositeChurn = (100 - churn) / 100f;

            return Mathf.Log(0.01f, oppositeChurn);
        }

        //public static int GetSegmentLoyalty(GameContext gameContext, GameEntity c, int segmentId)
        //{

        //}

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId, int segmentId) => GetChurnBonus(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static Bonus<long> GetChurnBonus(GameContext gameContext, GameEntity c, int segmentId)
        {
            var state = Markets.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = Products.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == MarketState.Death;


            var niche = Markets.Get(gameContext, c.product.Niche);
            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
            var baseValue = GetChurnRateBasedOnMonetisationType(monetisation);

            var retentionImprovement = (int)Products.GetChurnFeaturesBenefit(c);
            var monetisationImprovements = (int)Products.GetMonetisationFeatures(c).Length * 5;

            var audienceInfo = Marketing.GetAudienceInfos()[segmentId];

            var audienceBonus = audienceInfo.Bonuses.Where(b => b.isRetentionFeature).Sum(b => b.Max);

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base value for " + Enums.GetFormattedMonetisationType(monetisation), baseValue)
                .AppendAndHideIfZero("Base for this audience", (long)audienceBonus)

                .Append("Features", -retentionImprovement)
                .Append("Monetisation", monetisationImprovements)

                // technical stuff
                .AppendAndHideIfZero("Servers overload", Products.IsNeedsMoreServers(c) ? 30 : 0)
                .AppendAndHideIfZero("Not enough support", Products.IsNeedsMoreMarketingSupport(c) ? 15 : 0)

                // competition

                //.Append($"Concept difference to market ({fromProductLevel})", fromProductLevel * fromProductLevel)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(1, 100)
                ;
        }



        public static int GetChurnRateBasedOnMonetisationType(Monetisation monetisation)
        {
            return 5;
            //switch (monetisation)
            //{
            //    case Monetisation.Adverts:
            //        return 70;

            //    case Monetisation.Service:
            //        return 40;

            //    case Monetisation.Enterprise:
            //    case Monetisation.IrregularPaid:
            //    case Monetisation.Paid:
            //    default:
            //        return 20;
            //}
        }
    }
}
