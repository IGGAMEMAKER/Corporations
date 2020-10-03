using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
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

            var audiences = Marketing.GetAudienceInfos();

            var positionings = audiences.Select(a => new ProductPositioning
            {
                ID = 0,
                name = "",
                marketShare = 100,
                priceModifier = 1f,
                Loyalties = audiences.Select(ad => UnityEngine.Random.Range(-10, 10)).ToList(),
                isCompetitive = false,
            })
            .ToList();

            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(positionings);
            e.AddSegment(0);

            return e;
        }
    }
}
