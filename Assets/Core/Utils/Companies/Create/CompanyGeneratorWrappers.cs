using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    partial class Companies
    {
        // Create
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType)
        {
            var CEO = Humans.GenerateHuman(context);


            var level = UnityEngine.Random.Range(70, 90);

            Humans.SetTrait(CEO, Trait.Ambitious, level);
            Humans.SetSkills(CEO, WorkerRole.CEO);

            return CreateCompany(context, name, companyType, new Dictionary<int, BlockOfShares>(), CEO);
        }

        public static GameEntity GenerateCompanyGroup(GameContext context, string name, GameEntity formerProductCompany)
        {
            var group = GenerateCompanyGroup(context, name);

            CopyShareholders(formerProductCompany, group);

            return group;
        }

        public static void CopyShareholders(GameEntity From, GameEntity to)
        {
            ReplaceShareholders(to, From.shareholders.Shareholders);
        }


        public static GameEntity GenerateCompanyGroup(GameContext context, string name)
        {
            var c = CreateCompany(context, name, CompanyType.Group);
            c.isManagingCompany = true;

            Investments.BecomeInvestor(context, c, 0);

            return c;
        }

        public static GameEntity GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var c = CreateCompany(context, name, CompanyType.FinancialGroup);

            Investments.BecomeInvestor(context, c, money);

            return c;
        }

        public static GameEntity GenerateHoldingCompany(GameContext context, string name)
        {
            var c = GenerateCompanyGroup(context, name);

            return TurnToHolding(context, c.company.Id);
        }

        public static GameEntity GenerateProductCompany(GameContext context, string name, NicheType nicheType)
        {
            var c = CreateCompany(context, name, CompanyType.ProductCompany);

            return CreateProduct(context, c, nicheType);
        }

        public static GameEntity AutoGenerateProductCompany(NicheType nicheType, GameContext gameContext)
        {
            var playersOnMarket = Markets.GetCompetitorsAmount(nicheType, gameContext);

            var c = GenerateProductCompany(gameContext, Enums.GetFormattedNicheName(nicheType) + " " + playersOnMarket, nicheType);

            AutoFillShareholders(gameContext, c, true);
            //SetFounderAmbitionDueToMarketSize(c, gameContext);

            return c;
        }

        public static void AutoFillShareholders(GameContext gameContext, GameEntity c, bool founderOnly)
        {
            var founder = c.cEO.HumanId;
            var shareholder = Humans.Get(gameContext, founder);

            Investments.BecomeInvestor(gameContext, shareholder, 100000);

            AddShares(c, shareholder, 500);

            if (founderOnly)
                return;

            for (var i = 0; i < UnityEngine.Random.Range(1, 5); i++)
            {
                var investor = Investments.GetRandomInvestmentFund(gameContext);

                AddShares(c, investor, 100);
            }
        }

        public static void AutoFillNonFilledShareholders(GameContext gameContext, bool founderOnly)
        {
            var nonFinancialCompaniesWithZeroShareholders = Array.FindAll(gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Shareholders)),
                e => IsNotFinancialStructure(e) && e.shareholders.Shareholders.Count == 0);

            foreach (var c in nonFinancialCompaniesWithZeroShareholders)
                AutoFillShareholders(gameContext, c, founderOnly);
        }
    }
}
