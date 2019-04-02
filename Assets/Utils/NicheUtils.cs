using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public class NicheConstants
    {
        public static Dictionary<Niche, Industry> NicheMap = new Dictionary<Niche, Industry>
        {
            [Niche.OSCommonPurpose] = Industry.OS,
            [Niche.OSSciencePurpose] = Industry.OS,

            [Niche.Messenger] = Industry.Communications,
            [Niche.SocialNetwork] = Industry.Communications,

            [Niche.SearchEngine] = Industry.Search
        };
    }


    public static class NicheUtils
    {
        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameEntity e, GameContext context)
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
            var names = GetPlayersOnMarket(e, context)
                .Select(c => c.product.ProductLevel + "lvl - " + ProlongNameToNDigits(c.product.Name, 10));

            return names;
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(e, context).Count();
        }

        public static Industry GetIndustry(Niche niche)
        {
            return NicheConstants.NicheMap[niche];
        }
    }
}

public class MarketGenerator: IMarketGenerator
{
    public MarketGenerator()
    {
    }
}
