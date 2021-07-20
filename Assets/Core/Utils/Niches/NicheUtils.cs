using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static GameEntity[] GetNiches(GameContext context) => context.GetEntities(GameMatcher.Niche);

        public static GameEntity Get(GameContext context, GameEntity product) => Get(context, product.product.Niche);
        public static GameEntity Get(GameContext context, NicheType nicheType)
        {
            var e = Array.Find(GetNiches(context), n => n.niche.NicheType == nicheType);

            if (e == null)
                e = CreateNicheMockup(nicheType, context);

            return e;
        }

        public static List<float> CalculateMarketRequirements(GameEntity niche, GameContext gameContext, NewProductFeature[] allFeatures)
        {
            var competitors = Markets.GetProductsOnMarket(niche, gameContext);
            var features = allFeatures.ToList().Select(f => 0f).ToList();

            if (competitors.Any())
            {
                for (var i = 0; i < allFeatures.Length; i++)
                {
                    var f = allFeatures[i];

                    features[i] = competitors.Select(c => Products.GetFeatureRating(c, f.Name)).Max();
                }
            }

            return features;
        }

        public static MarketRequirementsComponent GetMarketRequirements(GameContext gameContext, GameEntity niche)
        {
            var allFeatures = Products.GetAllFeaturesForProduct();

            if (!niche.hasMarketRequirements)
                niche.AddMarketRequirements(allFeatures.Select(f => 0f).ToList());

            // is Empty list
            if (niche.marketRequirements.Features.Sum() == 0)
            {
            }
            niche.ReplaceMarketRequirements(CalculateMarketRequirements(niche, gameContext, allFeatures));

            return niche.marketRequirements;
        }

        public static MarketRequirementsComponent GetMarketRequirementsForCompany(GameContext gameContext, GameEntity c)
        {
            var niche = Markets.Get(gameContext, c);
            var reqs = Markets.GetMarketRequirements(gameContext, niche);

            if (!c.hasMarketRequirements)
            {
                /*var niche = Markets.Get(gameContext, c);
                var reqs = Markets.GetMarketRequirements(niche);*/

                c.AddMarketRequirements(reqs.Features);
            }

            return reqs;
            return c.marketRequirements;
        }

        public static GameEntity[] GetIndustries(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Industry);
        }

        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(GetNiches(context), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNiches(context);

            return Array.FindAll(niches, n => n.niche.IndustryType == industry);
        }




        public static GameEntity[] GetPlayableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);

            return Array.FindAll(niches, IsPlayableNiche);
        }

        public static GameEntity[] GetObservableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);

            return Array.FindAll(niches, IsObservableNiche);
        }

        public static GameEntity[] GetPlayableNiches(GameContext context)
        {
            return GetNiches(context)
                .Where(IsPlayableNiche)
                .ToArray();
        }



        public static bool IsPlayableNiche(GameContext gameContext, NicheType nicheType) => IsPlayableNiche(Get(gameContext, nicheType));
        public static bool IsPlayableNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase != MarketState.Idle && phase != MarketState.Death;
        }

        public static bool IsObservableNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return
                phase == MarketState.Trending ||
                phase == MarketState.Decay ||
                phase == MarketState.MassGrowth;
        }

        public static bool IsAppropriateStartNiche(GameEntity niche, GameContext gameContext)
        {
            var profile = niche.nicheBaseProfile.Profile;
            var phase = GetMarketState(niche);

            var isOpened = ScheduleUtils.GetCurrentDate(gameContext) >= niche.nicheLifecycle.OpenDate;
            var isPerspective = phase == MarketState.Idle || phase == MarketState.Innovation || phase == MarketState.Trending;

            var isCheap = profile.AppComplexity < AppComplexity.Hard;

            var isBig = profile.AudienceSize == AudienceSize.Global; // GetMarketPotentialRating(niche) > 3;


            return isOpened;
            return isPerspective && isOpened && isCheap && !isBig;
        }
    }
}
