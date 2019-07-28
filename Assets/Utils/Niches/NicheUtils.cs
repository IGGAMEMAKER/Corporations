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
            return Array.FindAll(GetNiches(context), n => n.niche.IndustryType == industry);
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



        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = GetPlayersOnMarket(gameContext, nicheType);

            return products
                .Select(p => CompanyEconomyUtils.GetCompanyCost(gameContext, p.company.Id))
                .Sum();
            
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }
    }
}
