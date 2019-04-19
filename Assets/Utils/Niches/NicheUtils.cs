using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetPlayersOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, GameEntity e)
        {
            return GetPlayersOnMarket(context, e.product.Niche);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, NicheType niche)
        {
            return context.GetEntities(GameMatcher.Product).Where(p => p.product.Niche == niche);
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, niche).Count();
        }

        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorInfo(GameEntity e, GameContext context)
        {
            var names = GetPlayersOnMarket(context, e)
                .Select(c => c.product.ProductLevel + "lvl - " + ProlongNameToNDigits(c.product.Name, 10));

            return names;
        }

        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(context.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            return Array.FindAll(context.GetEntities(GameMatcher.Niche), n => n.niche.IndustryType == industry);
        }

        public static bool IsBestAppOnNiche(GameContext gameContext, int companyId)
        {
            return GetLeaderApp(gameContext, companyId).company.Id == companyId;
        }

        private static GameEntity GetBestApp(IEnumerable<GameEntity> apps)
        {
            GameEntity best = null;

            foreach (var p in apps)
            {
                if (best == null || p.product.ProductLevel > best.product.ProductLevel)
                    best = p;
            }

            return best;
        }

        public static GameEntity GetLeaderApp(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetLeaderApp(gameContext, c.product.Niche);
        }

        public static GameEntity GetLeaderApp(GameContext gameContext, NicheType nicheType)
        {
            var competingProducts = GetPlayersOnMarket(gameContext, nicheType);

            return GetBestApp(competingProducts);
        }
    }
}
