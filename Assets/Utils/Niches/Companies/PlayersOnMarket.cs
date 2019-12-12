using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Markets
    {
        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            return GetProductsOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameEntity niche, GameContext context)
        {
            return GetProductsOnMarket(context, niche.niche.NicheType);
        }
        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, GameEntity product)
        {
            return GetProductsOnMarket(context, product.product.Niche);
        }

        public static GameEntity[] GetProductsOnMarket(GameContext context, NicheType niche, bool something)
        {
            return GetProductsOnMarket(context, niche).ToArray();
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, NicheType niche)
        {
            return Companies.GetProductCompanies(context).Where(p => p.product.Niche == niche);
        }

        public static bool IsEmptyMarket(GameContext gameContext, NicheType niche)
        {
            return GetCompetitorsAmount(niche, gameContext) == 0;
        }


        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetProductsOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetProductsOnMarket(context, niche).Count();
        }




        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }
    }
}
