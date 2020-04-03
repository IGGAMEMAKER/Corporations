using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Markets
    {
        // sphere of interests
        public static IEnumerable<GameEntity> GetNonFinancialCompaniesWithSameInterests(GameContext context, GameEntity company)
        {
            return GetCompaniesWithSameInterests(context, company).Where(Companies.IsNotFinancialStructure);
        }

        public static IEnumerable<GameEntity> GetFinancialStructuresWithSameInterests(GameContext context, GameEntity company)
        {
            return GetCompaniesWithSameInterests(context, company).Where(Companies.IsFinancialStructure);
        }

        public static IEnumerable<GameEntity> GetCompaniesWithSameInterests(GameContext context, GameEntity company)
        {
            var independent = Companies.GetIndependentCompanies(context);

            var interests = company.companyFocus.Niches;

            return independent.Where(c =>
            (c.hasProduct && interests.Contains(c.product.Niche))
            ||
            (c.hasCompanyFocus && c.companyFocus.Niches.Intersect(interests).Count() > 0)
            );
        }

        public static bool IsShareSameInterests(GameEntity c1, GameEntity c2)
        {
            var interests1 = c1.companyFocus.Niches;
            var interests2 = c2.companyFocus.Niches;

            return interests1.Intersect(interests2).Count() > 0;
        }

        public static IEnumerable<GameEntity> GetCompaniesInterestedInMarket(GameContext context, NicheType niche)
        {
            var independent = Companies.GetIndependentCompanies(context);

            return independent.Where(c =>
            (c.hasProduct && c.product.Niche == niche)
            ||
            (c.hasCompanyFocus && c.companyFocus.Niches.Contains(niche))
            );
        }
    }
}
