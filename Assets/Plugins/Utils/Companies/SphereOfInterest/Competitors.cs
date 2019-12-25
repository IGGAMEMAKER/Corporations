using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        internal static IEnumerable<GameEntity> GetCompetitorsOfCompany(GameEntity company, GameContext gameContext)
        {
            IEnumerable<GameEntity> companies;

            if (company.hasProduct)
                companies = Markets.GetProductsOnMarket(gameContext, company);
            else if (IsFinancialStructure(company))
            {
                companies = new GameEntity[0].AsEnumerable();
            }
            else
            {
                companies = Markets.GetNonFinancialCompaniesWithSameInterests(gameContext, company);
            }

            return companies.Where(p => p.company.Id != company.company.Id);
        }
    }
}
