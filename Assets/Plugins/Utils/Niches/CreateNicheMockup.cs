using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class Markets
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
                NicheSpeed = NicheSpeed.Year,
                Margin = Margin.Mid,
                MonetisationType = Monetisation.Service
            });

            var clientGrowthDictionary = new Dictionary<MarketState, int>
            {
                [MarketState.Idle] = 0, // 0
                [MarketState.Innovation] = UnityEngine.Random.Range(1, 3), // 2-5            Xt
                [MarketState.Trending] = UnityEngine.Random.Range(3, 10), // 4 - 10           5Xt
                [MarketState.MassGrowth] = UnityEngine.Random.Range(10, 15), // 7 - 15            10Xt
                [MarketState.Decay] = UnityEngine.Random.Range(2, 5), // 2 - 5 // churn      3Xt-22Xt
                [MarketState.Death] = 0, // churn
            };

            e.AddNicheLifecycle(0, clientGrowthDictionary, 0);

            e.AddNicheState(MarketState.Idle, 0);
            UpdateNicheDuration(e);


            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(new Dictionary<int, ProductPositioning>());
            e.AddSegment(0);

            return e;
        }
    }
}
