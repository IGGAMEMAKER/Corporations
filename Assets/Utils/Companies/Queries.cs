using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        internal static GameEntity[] GetIndependentCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.IndependentCompany, GameMatcher.Alive));
        }

        internal static GameEntity[] GetIndependentAICompanies(GameContext gameContext)
        {
            return GetIndependentCompanies(gameContext)
                .Where(c => !IsRelatedToPlayer(gameContext, c))
                .ToArray();
        }

        // products
        internal static GameEntity[] GetProductCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Product, GameMatcher.Alive));
        }

        internal static GameEntity[] GetDependentProducts(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Product, GameMatcher.Alive)
                .NoneOf(GameMatcher.IndependentCompany)
                );
        }

        internal static GameEntity[] GetAIProducts(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => !IsRelatedToPlayer(gameContext, p)
                );
        }

        internal static GameEntity[] GetPlayerRelatedProducts(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => IsRelatedToPlayer(gameContext, p)
                );
        }

        // groups
        internal static GameEntity[] GetAIManagingCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive)
                .NoneOf(GameMatcher.ControlledByPlayer));
        }

        internal static GameEntity[] GetGroupCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive));
        }

        internal static GameEntity[] GetNonFinancialCompanies(GameContext gameContext)
        {
            var investableCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.InvestmentProposals));

            return Array.FindAll(investableCompanies, IsNotFinancialStructure);
        }

        internal static GameEntity[] GetInvestmentFunds(GameContext gameContext)
        {
            var investingCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Shareholder));

            return Array.FindAll(investingCompanies, IsFinancialStructure);
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
