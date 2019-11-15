using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        // Create
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        public static int GenerateInvestorId(GameContext context)
        {
            return InvestmentUtils.GenerateInvestorId(context);
        }

        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType)
        {
            var CEO = HumanUtils.GenerateHuman(context);

            return CreateCompany(context, name, companyType, new Dictionary<int, BlockOfShares>(), CEO);
        }

        public static GameEntity GenerateCompanyGroup(GameContext context, string name, int FormerProductCompany)
        {
            var c = GenerateCompanyGroup(context, name);

            CopyShareholders(context, FormerProductCompany, c.company.Id);

            return c;
        }

        public static void CopyShareholders(GameContext gameContext, int from, int to)
        {
            var cFrom = GetCompanyById(gameContext, from);
            var cTo = GetCompanyById(gameContext, to);

            ReplaceShareholders(cTo, cFrom.shareholders.Shareholders);
        }


        public static GameEntity GenerateCompanyGroup(GameContext context, string name)
        {
            var c = CreateCompany(context, name, CompanyType.Group);
            c.isManagingCompany = true;

            InvestmentUtils.BecomeInvestor(context, c, 0);

            return c;
        }

        public static GameEntity GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var c = CreateCompany(context, name, CompanyType.FinancialGroup);

            InvestmentUtils.BecomeInvestor(context, c, money);

            return c;
        }

        public static GameEntity GenerateHoldingCompany(GameContext context, string name)
        {
            var c = GenerateCompanyGroup(context, name);

            return TurnToHolding(context, c.company.Id);
        }

        public static GameEntity GenerateProductCompany(GameContext context, string name, NicheType NicheType)
        {
            var c = CreateCompany(context, name, CompanyType.ProductCompany);

            return CreateProduct(context, c, NicheType);
        }

        public static GameEntity AutoGenerateProductCompany(NicheType NicheType, GameContext gameContext)
        {
            var playersOnMarket = NicheUtils.GetCompetitorsAmount(NicheType, gameContext);

            var c = GenerateProductCompany(gameContext, EnumUtils.GetFormattedNicheName(NicheType) + " " + playersOnMarket, NicheType);

            AutoFillShareholders(gameContext, c, true);
            SetFounderAmbitionDueToMarketSize(c, gameContext);

            return c;
        }

        public static void SetFounderAmbitionDueToMarketSize(GameEntity company, GameContext gameContext)
        {
            var niche = NicheUtils.GetNiche(gameContext, company.product.Niche);
            var rating = NicheUtils.GetMarketPotentialRating(niche);


            var rand = UnityEngine.Random.Range(1f, 2f);

            // 5...25      
            var ambition = 65 + Mathf.Clamp(rating * 5 * rand, 0, 30);
            var CeoId = GetCEOId(company);

            var currentAmbition = GetFounderAmbition(company, gameContext);

            var ceo = HumanUtils.GetHumanById(gameContext, CeoId);

            HumanUtils.SetTrait(ceo, TraitType.Ambitions, (int)ambition);
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
            var nonFinancialCompaniesWithZeroShareholders = Array.FindAll(gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Shareholders)),
                e => IsNotFinancialStructure(e) && e.shareholders.Shareholders.Count == 0);

            foreach (var c in nonFinancialCompaniesWithZeroShareholders)
                AutoFillShareholders(gameContext, c, founderOnly);
        }
    }
}
