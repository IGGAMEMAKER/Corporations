using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static GameEntity[] GetNiches(GameContext context)
        {
            return context.GetEntities(GameMatcher.Niche);
        }

        public static GameEntity[] GetPerspectiveNiches(GameContext context)
        {
            return GetPlayableNiches(context)
                .Where(n => IsPerspectiveNiche(n))
                .ToArray();
        }

        public static GameEntity[] GetPlayableNiches(GameContext context)
        {
            return GetNiches(context)
                .Where(n => IsPlayableNiche(n))
                .ToArray();
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
            return Array.FindAll(GetNiches(context), n => n.niche.IndustryType == industry);
        }

        public static GameEntity[] GetPlayableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);

            return Array.FindAll(niches, n => IsPlayableNiche(n));
        }

        public static GameEntity GetNicheEntity(GameContext context, NicheType nicheType)
        {
            var e = Array.Find(GetNiches(context), n => n.niche.NicheType == nicheType);

            if (e == null)
                e = CreateNicheMockup(nicheType, context);

            return e;
        }


        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return IsPerspectiveNiche(niche);
        }

        public static bool IsPerspectiveNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase == NicheLifecyclePhase.Innovation || phase == NicheLifecyclePhase.Trending;
        }

        public static bool IsPlayableNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return IsPlayableNiche(niche);
        }

        public static bool IsPlayableNiche(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            return phase != NicheLifecyclePhase.Idle && phase != NicheLifecyclePhase.Death;
        }




        internal static void AddNewUsersToMarket(GameEntity niche, GameContext gameContext, long clients)
        {
            var nicheType = niche.niche.NicheType;

            var segments = GetNichePositionings(nicheType, gameContext);

            var clientContainers = niche.nicheClientsContainer.Clients;
            foreach (var s in segments)
            {
                // clients to clientContainer

                var segId = s.Key;

                var share = s.Value.marketShare;

                clientContainers[segId] += clients * share / 100;
            }

            niche.ReplaceNicheClientsContainer(clientContainers);
        }

        internal static void ReturnUsersWhenCompanyIsClosed(GameEntity e, GameContext gameContext)
        {
            var users = MarketingUtils.GetClients(e);

            var niche = GetNicheEntity(gameContext, e.product.Niche);

            AddNewUsersToMarket(niche, gameContext, users);

            MarketingUtils.AddClients(e, -users);
        }
    }
}
