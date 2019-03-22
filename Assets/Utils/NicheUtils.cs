using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static class NicheUtils
    {
        public static IEnumerable<GameEntity> GetCompetitors(GameEntity e, GameContext context)
        {
            return context.GetEntities(GameMatcher.Product).Where(p => p.product.Niche == e.product.Niche);
        }

        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorInfo(GameEntity e, GameContext context)
        {
            var names = GetCompetitors(e, context)
                .Select(c => c.product.ProductLevel + "lvl - " + ProlongNameToNDigits(c.product.Name, 10));

            return names;
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetCompetitors(e, context).Count();
        }
    }
}
