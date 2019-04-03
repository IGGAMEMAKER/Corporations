using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public class NicheConstants
    {
        public static Dictionary<NicheType, IndustryType> NicheMap = new Dictionary<NicheType, IndustryType>
        {
            [NicheType.OSCommonPurpose] = IndustryType.OS,
            [NicheType.OSSciencePurpose] = IndustryType.OS,

            [NicheType.Messenger] = IndustryType.Communications,
            [NicheType.SocialNetwork] = IndustryType.Communications,

            [NicheType.SearchEngine] = IndustryType.Search
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

        public static IndustryType GetIndustry(NicheType niche)
        {
            return NicheConstants.NicheMap[niche];
        }
    }
}
