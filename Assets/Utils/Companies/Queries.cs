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

        public static void AutoFillShareholders(GameContext gameContext, GameEntity c, bool founderOnly)
        {
            var founder = c.cEO.HumanId;
            var shareholder = HumanUtils.GetHumanById(gameContext, founder);

            InvestmentUtils.BecomeInvestor(gameContext, shareholder, 100000);

            AddShareholder(gameContext, c.company.Id, shareholder.shareholder.Id, 500);

            if (founderOnly)
                return;

            for (var i = 0; i < UnityEngine.Random.Range(1, 5); i++)
            {
                int investorId = InvestmentUtils.GetRandomInvestmentFund(gameContext);

                AddShareholder(gameContext, c.company.Id, investorId, 100);
            }
        }

        public static void AutoFillNonFilledShareholders(GameContext gameContext, bool founderOnly)
        {
            foreach (var c in GetNonFinancialCompaniesWithZeroShareholders(gameContext))
            {
                AutoFillShareholders(gameContext, c, founderOnly);
            }
        }
    }
}
