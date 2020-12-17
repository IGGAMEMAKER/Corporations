using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity[] GetIndependentCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.IndependentCompany, GameMatcher.Alive));
        }

        public static GameEntity[] GetIndependentAICompanies(GameContext gameContext)
        {
            return GetIndependentCompanies(gameContext)
                .Where(c => !IsDirectlyRelatedToPlayer(gameContext, c))
                .ToArray();
        }

        // products
        public static GameEntity[] GetProductCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Product, GameMatcher.Alive));
        }

        public static GameEntity[] GetDependentProducts(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Product, GameMatcher.Alive)
                .NoneOf(GameMatcher.IndependentCompany)
                );
        }

        public static GameEntity[] GetAIProducts(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => !IsDirectlyRelatedToPlayer(gameContext, p)
                );
        }

        public static GameEntity[] GetPlayerRelatedProducts(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => IsDirectlyRelatedToPlayer(gameContext, p)
                );
        }

        public static GameEntity[] GetPlayerRelatedCompanies(GameContext gameContext)
        {
            var companies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Alive));

            return Array.FindAll(companies,
                p => IsDirectlyRelatedToPlayer(gameContext, p)
                );
        }



        // groups
        public static GameEntity[] GetAIManagingCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive)
                .NoneOf(GameMatcher.ControlledByPlayer));
        }

        public static GameEntity[] GetGroupCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive));
        }

        public static GameEntity[] GetNonFinancialCompanies(GameContext gameContext)
        {
            var investableCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.InvestmentProposals));

            return Array.FindAll(investableCompanies, IsNotFinancialStructure);
        }

        public static GameEntity[] GetInvestmentFunds(GameContext gameContext)
        {
            var investingCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Shareholder));

            return Array.FindAll(investingCompanies, IsFinancialStructure);
        }

        public static IEnumerable<GameEntity> GetInvestmentFundsWhoAreInterestedInMarket(GameContext gameContext, NicheType niche)
        {
            return GetInvestmentFunds(gameContext)
                .Where(f => f.companyFocus.Niches.Contains(niche));
        }



        public static bool IsNotFinancialStructure(GameEntity e)
        {
            return !IsFinancialStructure(e);
        }

        public static bool IsFinancialStructure(GameEntity e)
        {
            return e.company.CompanyType == CompanyType.FinancialGroup;
        }
    }
}
