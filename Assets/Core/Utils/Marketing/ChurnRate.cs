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

        public static float GetSegmentLoyalty(GameContext gameContext, GameEntity c, int segmentId) => GetSegmentLoyalty(gameContext, c, segmentId, true).Sum();
        public static Bonus<long> GetSegmentLoyalty(GameContext gameContext, GameEntity c, int segmentId, bool isBonus)
        {
            var features = Products.GetAvailableFeaturesForProduct(c);
            var infos = GetAudienceInfos();

            var segment = infos[segmentId];

            var bonus = new Bonus<long>("Loyalty");

            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(c, f.Name))
                {
                    var rating = Products.GetFeatureRating(c, f.Name);
                    var attitude = f.AttitudeToFeature[segmentId];

                    var loyaltyGain = 0f;

                    if (attitude >= 0)
                    {
                        loyaltyGain = rating * attitude / 10;
                    }
                    else
                    {
                        loyaltyGain = attitude + (10 - rating) * attitude / 10;
                    }

                    bonus.Append($"Feature {f.Name}", (int)loyaltyGain);
                }
            }

            bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(c) ? -70 : 0);
            bonus.AppendAndHideIfZero("Not enough support", Products.IsNeedsMoreMarketingSupport(c) ? -7 : 0);



            bonus.Cap(-100, 50);

            return bonus;
        }

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId, int segmentId) => GetChurnBonus(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static Bonus<long> GetChurnBonus(GameContext gameContext, GameEntity c, int segmentId)
        {
            var state = Markets.GetMarketState(gameContext, c.product.Niche);

            var marketIsDying = state == MarketState.Death;

            var loyalty = GetSegmentLoyalty(gameContext, c, segmentId);

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")

                .AppendAndHideIfZero("Disloyal clients", loyalty < 0 ? 5 : 0)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(0, 100);
        }
    }
}
