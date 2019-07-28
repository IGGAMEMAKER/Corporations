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
                NicheType.SocialNetwork
                );


            e.AddNicheCosts(1, 1, 1, 1, 1, 1);

            var clientGrowthDictionary = new Dictionary<NicheLifecyclePhase, int>
            {
                [NicheLifecyclePhase.Idle] = 0, // 0
                [NicheLifecyclePhase.Innovation] = UnityEngine.Random.Range(1, 3), // 2-5            Xt
                [NicheLifecyclePhase.Trending] = UnityEngine.Random.Range(3, 10), // 4 - 10           5Xt
                [NicheLifecyclePhase.MassUse] = UnityEngine.Random.Range(10, 15), // 7 - 15            10Xt
                [NicheLifecyclePhase.Decay] = UnityEngine.Random.Range(2, 5), // 2 - 5 // churn      3Xt-22Xt
                [NicheLifecyclePhase.Death] = 0, // churn
            };

            e.AddNicheLifecycle(0, clientGrowthDictionary, NicheDuration.EntireGame);

            e.AddNicheState(NicheLifecyclePhase.Innovation, 0);

            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(new Dictionary<int, ProductPositioning>());


            e.AddSegment(new Dictionary<UserType, int>
            {
                [UserType.Core] = 1,
                [UserType.Regular] = 1,
                [UserType.Mass] = 1,
            });

            return e;
        }


        // durations
        public static int GetNichePeriodDurationInMonths(GameEntity niche)
        {
            NicheDuration nicheDuration = niche.nicheLifecycle.Period;

            bool isEntireGame = nicheDuration == NicheDuration.EntireGame;
            //isEntireGame ? 10000

            var X = 1;

            return X;
        }

        public static int GetMinimumPhaseDurationInPeriods(NicheLifecyclePhase phase)
        {
            //if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
            //    return 0;

            //return 1;


            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return 1;

                case NicheLifecyclePhase.Trending:
                    return 4;

                case NicheLifecyclePhase.MassUse:
                    return 15;

                case NicheLifecyclePhase.Decay:
                    return 10;

                default:
                    return 0;
            }
        }

    }
}
