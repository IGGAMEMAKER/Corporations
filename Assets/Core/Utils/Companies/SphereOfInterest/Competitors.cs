using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static IEnumerable<GameEntity> GetDirectCompetitors(GameEntity company, GameContext gameContext, bool includeSelf = false)
        {
            //var competitors = GetCompetitorsOf(company, gameContext, includeSelf);

            //return competitors.Where(c => c.productPositioning.Positioning == company.productPositioning.Positioning);
            return GetCompetitionInSegment(company, gameContext, company.productPositioning.Positioning, includeSelf);
        }

        public static IEnumerable<GameEntity> GetCompetitionInSegment(GameEntity company, GameContext gameContext, int positioningID, bool includeSelf = false)
        {
            var competitors = GetCompetitorsOf(company, gameContext, includeSelf);

            return competitors.Where(c => c.productPositioning.Positioning == positioningID);
        }

        public static bool IsDirectCompetitor(GameEntity c1, GameEntity c2) => c1.productPositioning.Positioning == c2.productPositioning.Positioning && c1.product.Niche == c2.product.Niche;

        public static IEnumerable<GameEntity> GetCompetitorsOf(GameEntity company, GameContext gameContext, bool includeSelf = false)
        {
            IEnumerable<GameEntity> companies;

            if (company.hasProduct)
            {
                companies = Markets.GetProductsOnMarket(gameContext, company);
            }
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


        public static List<GameEntity> GetSortedCompetitors(GameEntity company, GameContext gameContext, ref int index)
        {
            // first - Strong
            // last - Weak

            var competitors = GetCompetitorsOf(company, gameContext, true)
        .OrderByDescending(c => Economy.CostOf(c, gameContext))
        .ToList();

            index = competitors.FindIndex(c => c.company.Id == company.company.Id);

            return competitors;
        }

        public static GameEntity GetStrongerCompetitor(GameEntity company, GameContext gameContext)
        {
            int index = 0;
            var competitors = GetSortedCompetitors(company, gameContext, ref index);

            if (index == 0)
                return null;

            return competitors[index - 1];
        }

        public static GameEntity GetWeakerCompetitor(GameEntity company, GameContext gameContext)
        {
            int index = 0;
            var competitors = GetSortedCompetitors(company, gameContext, ref index);

            if (index + 1 >= competitors.Count)
                return null;

            return competitors.Last();
        }
    }
}
