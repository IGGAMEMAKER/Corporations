using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static GameEntity GetNicheEntity(GameContext gameContext, NicheType nicheType)
        {
            var e = Array.Find(gameContext.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == nicheType);

            if (e == null)
                e = CreateNicheMockup(nicheType, gameContext);

            return e;
        }

        public static NicheLifecyclePhase GetMarketState(GameContext gameContext, NicheType nicheType)
        {
            return GetNicheEntity(gameContext, nicheType).nicheState.Phase;
        }

        public static GameEntity[] GetIndustries(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Industry);
        }

        public static GameEntity[] GetNiches(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Niche);
        }



        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(context.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            return Array.FindAll(context.GetEntities(GameMatcher.Niche), n => n.niche.IndustryType == industry);
        }

        public static GameEntity[] GetPlayableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);
            return Array.FindAll(niches, n => IsPlayableNiche(context, n));
        }

        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            var phase = niche.nicheState.Phase;

            return phase == NicheLifecyclePhase.Innovation ||
                phase == NicheLifecyclePhase.Trending ||
                phase == NicheLifecyclePhase.MassUse;
        }

        public static bool IsPlayableNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return IsPlayableNiche(gameContext, niche);
        }

        public static bool IsPlayableNiche(GameContext gameContext, GameEntity niche)
        {
            var phase = niche.nicheState.Phase;

            return phase != NicheLifecyclePhase.Idle && phase != NicheLifecyclePhase.Death;
        }



        public static int GetMarketRating(GameContext gameContext, NicheType niche)
        {
            return GetMarketRating(GetNicheEntity(gameContext, niche));
        }

        public static int GetMarketRating(GameEntity niche)
        {
            switch (niche.nicheState.Phase)
            {
                case NicheLifecyclePhase.Idle: return 1;
                case NicheLifecyclePhase.Innovation: return 3;
                case NicheLifecyclePhase.Trending: return 4;
                case NicheLifecyclePhase.MassUse: return 5;
                case NicheLifecyclePhase.Decay: return 2;

                default:
                    return 0;
            }
        }

        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = NicheUtils.GetPlayersOnMarket(gameContext, nicheType);

            return products.Select(p => CompanyEconomyUtils.GetCompanyCost(gameContext, p.company.Id)).Sum();
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }



        // months
        public static int GetNichePeriodDuration(GameEntity niche)
        {
            var X = 1;

            return X;
        }

        public static int GetMinimumPhaseDurationInPeriods(NicheLifecyclePhase phase)
        {
            if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
                return 0;

            return 1;

            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return 1;

                case NicheLifecyclePhase.Trending:
                    return 4;

                case NicheLifecyclePhase.MassUse:
                    return 10;

                case NicheLifecyclePhase.Decay:
                    return 15;

                default:
                    return 0;
            }
        }



        public static List<GameEntity> GetProductsAvailableForSaleInSphereOfInfluence(GameEntity managingCompany, GameContext context)
        {
            List<GameEntity> products = new List<GameEntity>();

            var niches = managingCompany.companyFocus.Niches;

            foreach (var n in niches)
            {
                var companies = GetProductsAvailableForSaleOnMarket(n, context);

                products.AddRange(companies);
            }

            return products.FindAll(p => !CompanyUtils.IsCompanyRelatedToPlayer(context, p));
        }

        public static GameEntity[] GetProductsAvailableForSaleOnMarket(NicheType n, GameContext context)
        {
            return GetPlayersOnMarket(context, n)
                .Where(p => CompanyUtils.IsWillSellCompany(p, context) && p.isAlive && p.companyGoal.InvestorGoal != InvestorGoal.Prototype)
                .ToArray();
        }
    }
}
