﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static IEnumerable<GameEntity> GetDirectCompetitors(GameEntity company, GameContext gameContext, bool includeSelf = false)
        {
            var competitors = GetCompetitorsOfCompany(company, gameContext, includeSelf);

            return competitors.Where(c => c.productPositioning.Positioning == company.productPositioning.Positioning);
        }

        public static IEnumerable<GameEntity> GetCompetitionInSegment(GameEntity company, GameContext gameContext, int positioningID, bool includeSelf = false)
        {
            var competitors = GetCompetitorsOfCompany(company, gameContext, includeSelf);

            return competitors.Where(c => c.productPositioning.Positioning == positioningID);
        }

        public static IEnumerable<GameEntity> GetCompetitorsOfCompany(GameEntity company, GameContext gameContext, bool includeSelf = false)
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

        public static GameEntity GetStrongerCompetitor(GameEntity company, GameContext gameContext)
        {
            var competitors = Companies.GetCompetitorsOfCompany(company, gameContext, true).OrderByDescending(c => Economy.CostOf(c, gameContext)).ToList();
            var index = competitors.FindIndex(c => c.company.Id == company.company.Id);

            var nearestCompetitor = Companies.GetCompetitorsOfCompany(company, gameContext, true);

            if (index == 0)
                return null;

            return competitors[index - 1];
        }

        public static GameEntity GetWeakerCompetitor(GameEntity company, GameContext gameContext)
        {
            var competitors = Companies.GetCompetitorsOfCompany(company, gameContext, true).OrderByDescending(c => Economy.CostOf(c, gameContext)).ToList();
            var index = competitors.FindIndex(c => c.company.Id == company.company.Id);

            var nearestCompetitor = Companies.GetCompetitorsOfCompany(company, gameContext, true);

            if (index + 1 >= competitors.Count)
                return null;

            return competitors.Last();
        }
    }
}
