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


            e.AddNicheCosts(1, 1, 1, 1, 1, 1);
            e.AddNicheBaseProfile(new MarketProfile {
                AppComplexity = AppComplexity.Mid,
                AudienceSize = AudienceSize.Million,
                Iteration = NicheSpeed.Year,
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

            e.AddNicheLifecycle(0, clientGrowthDictionary, NicheDuration.EntireGame, NicheSpeed.Year);

            e.AddNicheState(NicheLifecyclePhase.Idle, 0);
            UpdateNicheDuration(e);


            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(new Dictionary<int, ProductPositioning>());


            e.AddSegment(new Dictionary<UserType, int>
            {
                [UserType.Core] = 0,
            });

            return e;
        }




        // durations
        public static int GetNicheDuration(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            var duration = GetMinimumPhaseDurationInPeriods(phase) * GetNichePeriodDurationInMonths(niche);

            return duration;
        }

        public static void UpdateNicheDuration(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            var newDuration = GetNicheDuration(niche);

            niche.ReplaceNicheState(phase, newDuration);
        }

        public static void PromoteNicheState(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            var next = GetNextPhase(phase);

            var newDuration = GetNichePeriodDurationInMonths(niche.nicheLifecycle.Period, next);
            niche.ReplaceNicheState(next, newDuration);
        }


        public static int GetNichePeriodDurationInMonths(GameEntity niche)
        {
            NicheDuration nicheDuration = niche.nicheLifecycle.Period;

            var state = GetMarketState(niche);

            return GetNichePeriodDurationInMonths(nicheDuration, state);
        }

        public static int GetNichePeriodDurationInMonths(NicheDuration nicheDuration, NicheLifecyclePhase phase)
        {
            if (nicheDuration == NicheDuration.EntireGame)
            {
                switch (phase)
                {
                    case NicheLifecyclePhase.Innovation: return 12;
                    case NicheLifecyclePhase.Trending: return 24;
                    case NicheLifecyclePhase.MassUse: return 1000;
                    case NicheLifecyclePhase.Decay: return 1000;
                    default: return 0;
                }
            }

            var durationInMonths = (int)nicheDuration;

            var sumOfPeriods = NICHE_PHASE_DURATION_INNOVATION + NICHE_PHASE_DURATION_TRENDING + NICHE_PHASE_DURATION_MASS + NICHE_PHASE_DURATION_DECAY;
            
            var X = durationInMonths / sumOfPeriods;

            return Math.Max(X, 1);
        }

        public static int GetMinimumPhaseDurationInPeriods(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return NICHE_PHASE_DURATION_INNOVATION;

                case NicheLifecyclePhase.Trending:
                    return NICHE_PHASE_DURATION_TRENDING;

                case NicheLifecyclePhase.MassUse:
                    return NICHE_PHASE_DURATION_MASS;

                case NicheLifecyclePhase.Decay:
                    return NICHE_PHASE_DURATION_DECAY;

                default:
                    return 0;
            }
        }

        public static NicheLifecyclePhase GetNextPhase(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Idle:
                    return NicheLifecyclePhase.Innovation;

                case NicheLifecyclePhase.Innovation:
                    return NicheLifecyclePhase.Trending;

                case NicheLifecyclePhase.Trending:
                    return NicheLifecyclePhase.MassUse;

                case NicheLifecyclePhase.MassUse:
                    return NicheLifecyclePhase.Decay;

                case NicheLifecyclePhase.Decay:
                default:
                    return NicheLifecyclePhase.Death;
            }
        }

        public const int NICHE_PHASE_DURATION_INNOVATION = 1;
        public const int NICHE_PHASE_DURATION_TRENDING = 4;
        public const int NICHE_PHASE_DURATION_MASS = 15;
        public const int NICHE_PHASE_DURATION_DECAY = 10;
    }
}
