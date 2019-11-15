using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
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
            return GetProductsOnMarket(context, n)
                .Where(p => CompanyUtils.IsWillSellCompany(p, context))
                .ToArray();
        }
    }
}
