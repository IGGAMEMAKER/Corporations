using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        internal static IEnumerable<GameEntity> GetCompetitorsOfCompany(GameEntity company, GameContext gameContext, bool includeSelf = false)
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

            return includeSelf ? companies : companies.Where(p => p.company.Id != company.company.Id);
        }
    }
}
