using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static GameEntity[] GetProductCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.Alive));
        }

        internal static GameEntity[] GetIndependentCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.IndependentCompany, GameMatcher.Alive));
        }


        internal static GameEntity[] GetAIProducts(GameContext gameContext)
        {
            return GetProductCompanies(gameContext);

            return gameContext.GetEntities(
                    GameMatcher
                    .AllOf(GameMatcher.Product, GameMatcher.Alive)
                //.NoneOf(GameMatcher.ControlledByPlayer)
                );
        }

        internal static GameEntity[] GetGroupCompanies(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholder)),
                e => IsManagingCompany(e)
            );
        }

        internal static GameEntity[] GetAIManagingCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(
                GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive)
                .NoneOf(GameMatcher.ControlledByPlayer)
                );
        }

        internal static GameEntity[] GetInvestmentFunds(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholder)),
                e => e.company.CompanyType == CompanyType.FinancialGroup
            );
        }

        internal static GameEntity[] GetNonFinancialCompaniesWithZeroShareholders(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders)),
                e => e.shareholders.Shareholders.Count == 0 && e.company.CompanyType != CompanyType.FinancialGroup
            );
        }

        internal static GameEntity[] GetNonFinancialCompanies(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.InvestmentProposals)),
                e => e.company.CompanyType != CompanyType.FinancialGroup
            );
        }
    }
}
