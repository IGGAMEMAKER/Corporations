using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static List<float> GetEmptyMarketRequirements() => Products.GetAllFeaturesForProduct().Select(f => 0f).ToList();

        public static MarketRequirementsComponent GetMarketRequirements(GameContext gameContext, GameEntity niche)
        {
            var allFeatures = Products.GetAllFeaturesForProduct();

            if (!niche.hasMarketRequirements)
                niche.AddMarketRequirements(GetEmptyMarketRequirements());

            niche.ReplaceMarketRequirements(GetCalculatedMarketRequirements(niche, gameContext, allFeatures));

            return niche.marketRequirements;
        }

        public static MarketRequirementsComponent GetMarketRequirementsForCompany(GameContext gameContext, GameEntity c)
        {
            var niche = Markets.Get(gameContext, c);
            var reqs = Markets.GetMarketRequirements(gameContext, niche);

            if (!c.hasMarketRequirements)
                c.AddMarketRequirements(reqs.Features);

            return reqs;
        }

        public static float GetMaxFeatureLVL(IEnumerable<GameEntity> competitors, string featureName)
        {
            return competitors.Select(c => Products.GetFeatureRating(c, featureName)).Max();
        }

        public static List<float> GetCalculatedMarketRequirements(GameEntity niche, GameContext gameContext) => GetCalculatedMarketRequirements(niche, gameContext, Products.GetAllFeaturesForProduct());
        public static List<float> GetCalculatedMarketRequirements(GameEntity niche, GameContext gameContext, NewProductFeature[] allFeatures)
        {
            return GetCalculatedMarketRequirements(GetProductsOnMarket(niche, gameContext), allFeatures);
        }

        public static List<float> GetCalculatedMarketRequirements(IEnumerable<GameEntity> competitors, NewProductFeature[] allFeatures)
        {
            var features = GetEmptyMarketRequirements();

            if (competitors.Any())
            {
                for (var i = 0; i < allFeatures.Length; i++)
                    features[i] = GetMaxFeatureLVL(competitors, allFeatures[i].Name);
            }

            return features;
        }


        public static List<float> CopyMarketRequirements(List<float> features)
        {
            var LastFeatures = new List<float>();

            for (var i = 0; i < features.Count; i++)
            {
                if (LastFeatures.Count == i)
                    LastFeatures.Add(0);

                LastFeatures[i] = features[i];
            }

            return LastFeatures;
        }
    }
}
