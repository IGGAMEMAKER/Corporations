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

        public static float GetSegmentLoyalty(GameContext gameContext, GameEntity c, int segmentId)
        {
            var features = Products.GetAvailableFeaturesForProduct(c);
            var infos = GetAudienceInfos();

            var segment = infos[segmentId];

            var bonus = new Bonus<long>("Loyalty");

            float loyalty = 0f;

            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(c, f.Name))
                {
                    bonus.Append($"Feature {f.Name}", (int)Products.GetFeatureRating(c, f.Name) * f.AttitudeToFeature[segmentId]);
                }
            }

            bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(c) ? -70 : 0);
            bonus.AppendAndHideIfZero("Not enough support", Products.IsNeedsMoreMarketingSupport(c) ? -7 : 0);

            bonus.Cap(-100, 50);

            return loyalty;
        }

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId, int segmentId) => GetChurnBonus(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static Bonus<long> GetChurnBonus(GameContext gameContext, GameEntity c, int segmentId)
        {
            var state = Markets.GetMarketState(gameContext, c.product.Niche);

            var marketIsDying = state == MarketState.Death;


            //var niche = Markets.Get(gameContext, c.product.Niche);
            //var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
            //var baseValue = GetChurnRateBasedOnMonetisationType(monetisation);

            var loyalty = GetSegmentLoyalty(gameContext, c, segmentId);

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                //.Append("Base value for " + Enums.GetFormattedMonetisationType(monetisation), baseValue)

                // loyalty
                .AppendAndHideIfZero("Disloyal clients", loyalty < 0 ? 5 : 0)

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
