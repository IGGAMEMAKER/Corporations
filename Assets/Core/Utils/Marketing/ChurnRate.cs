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

            var positioningId = c.productPositioning.Positioning;
            var positionings = Marketing.GetProductPositionings(c, gameContext)[positioningId];
            
            var positioningBonus = positionings.Loyalties[segmentId];

            var segment = infos[segmentId];

            var bonus = new Bonus<long>("Loyalty");

            foreach (var f in features)
            {
                if (Products.IsUpgradedFeature(c, f.Name))
                {
                    var rating = Products.GetFeatureRating(c, f.Name);
                    var attitude = f.AttitudeToFeature[segmentId];

                    var loyaltyGain = GetLoyaltyChangeFromFeature(c, f, segmentId, false);

                    bonus.Append($"Feature {f.Name}", (int)loyaltyGain);
                }
            }

            bonus.AppendAndHideIfZero("From positioning", positioningBonus);

            //var competitors = Companies.GetCompetitorsOfCompany(c, gameContext, false);
            //bool isOutcompeted = false;

            //if (competitors.Count() > 0)
            //{
            //    var maxLoyalty = competitors.Select(competitor => GetSegmentLoyalty(gameContext, competitor, segmentId)).Max();

            //    isOutcompeted = maxLoyalty > loyalty + 5;
            //}

            bonus.AppendAndHideIfZero("Server overload", Products.IsNeedsMoreServers(c) ? -70 : 0);
            //bonus.AppendAndHideIfZero("Is outcompeted (loyalty way worse than competitors)", isOutcompeted ? -25 : 0);
            //bonus.AppendAndHideIfZero("Not enough support", Products.IsNeedsMoreMarketingSupport(c) ? -7 : 0);



            bonus.Cap(-100, 50);

            return bonus;
        }

        // if maxChange = true
        // this will return max loyalty for regular features
        // and worst loyalty hit for monetisation features

        // otherwise - upgraded values?
        public static float GetLoyaltyChangeFromFeature(GameEntity c, NewProductFeature f, int segmentId, bool maxChange = false)
        {
            var rating = Products.GetFeatureRating(c, f.Name);
            var attitude = f.AttitudeToFeature[segmentId];

            if (maxChange)
            {
                if (attitude <= 0)
                {
                    rating = 0;
                }
                else
                {
                    rating = 10;
                }
            }

            var loyaltyGain = 0f;

            if (attitude >= 0)
            {
                loyaltyGain = rating * attitude / 10;
            }
            else
            {
                loyaltyGain = attitude + (10 - rating) * attitude / 10;
            }

            return loyaltyGain;
        }

        public static Bonus<long> GetChurnBonus(GameContext gameContext, int companyId, int segmentId) => GetChurnBonus(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static Bonus<long> GetChurnBonus(GameContext gameContext, GameEntity c, int segmentId)
        {
            var state = Markets.GetMarketState(gameContext, c.product.Niche);

            var marketIsDying = state == MarketState.Death;

            var loyalty = GetSegmentLoyalty(gameContext, c, segmentId);

            var competitors = Companies.GetCompetitorsOfCompany(c, gameContext, false);
            bool isOutcompeted = false;

            if (competitors.Count() > 0)
            {
                var maxLoyalty = competitors.Select(competitor => GetSegmentLoyalty(gameContext, competitor, segmentId)).Max();

                isOutcompeted = maxLoyalty > loyalty + 5;
            }

            return new Bonus<long>("Churn rate")
                .RenderTitle()
                .SetDimension("%")

                .AppendAndHideIfZero("Disloyal clients", loyalty < 0 ? 5 : 0)
                //.AppendAndHideIfZero("Outcompeted (loyalty difference > 5)", isOutcompeted ? 5 : 0)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0)
                .Cap(0, 100);
        }
    }
}
