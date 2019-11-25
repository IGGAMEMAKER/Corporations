using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static GameEntity CreateNicheMockup(NicheType niche, GameContext GameContext)
        {
            var e = GameContext.CreateEntity();

            e.AddNiche(
                niche,
                IndustryType.Communications,
                new List<MarketCompatibility>(),
                new List<NicheType>(),
                NicheType.Com_SocialNetwork
                );


            e.AddNicheCosts(1, 1, 1, 1);
            e.AddNicheBaseProfile(new MarketProfile {
                AppComplexity = AppComplexity.Average,
                AudienceSize = AudienceSize.Million,
                NicheSpeed = NichePeriod.Year,
                Margin = Margin.Mid,
                MonetisationType = Monetisation.Service
            });

            var clientGrowthDictionary = new Dictionary<NicheLifecyclePhase, int>
            {
                [NicheLifecyclePhase.Idle] = 0, // 0
                [NicheLifecyclePhase.Innovation] = UnityEngine.Random.Range(1, 3), // 2-5            Xt
                [NicheLifecyclePhase.Trending] = UnityEngine.Random.Range(3, 10), // 4 - 10           5Xt
                [NicheLifecyclePhase.MassUse] = UnityEngine.Random.Range(10, 15), // 7 - 15            10Xt
                [NicheLifecyclePhase.Decay] = UnityEngine.Random.Range(2, 5), // 2 - 5 // churn      3Xt-22Xt
                [NicheLifecyclePhase.Death] = 0, // churn
            };

            e.AddNicheLifecycle(0, clientGrowthDictionary, NicheDuration.EntireGame, NichePeriod.Year);

            e.AddNicheState(NicheLifecyclePhase.Idle, 0);
            UpdateNicheDuration(e);


            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(new Dictionary<int, ProductPositioning>());
            e.AddSegment(0);

            return e;
        }

        // update
        public static void PromoteNicheState(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            var next = GetNextPhase(phase);

            var newDuration = GetNichePeriodDurationInMonths(niche.nicheLifecycle.Period, next);
            niche.ReplaceNicheState(next, newDuration);
        }
    }
}
