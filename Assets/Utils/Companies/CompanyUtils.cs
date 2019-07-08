﻿using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // Read
        public static GameEntity GetCompanyById(GameContext context, int companyId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);
        }

        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return InvestmentUtils.GetInvestorById(context, investorId);
        }

        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return InvestmentUtils.GetCompanyIdByInvestorId(context, investorId);
        }

        internal static GameEntity[] GetProductCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Product);
        }

        internal static GameEntity[] GetAIProducts(GameContext gameContext)
        {
            return gameContext.GetEntities(
                    GameMatcher
                    .AllOf(GameMatcher.Product, GameMatcher.Alive)
                //.NoneOf(GameMatcher.ControlledByPlayer)
                );
        }

        internal static GameEntity[] GetAIManagingCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(
                GameMatcher
                .AllOf(GameMatcher.ManagingCompany)
                .NoneOf(GameMatcher.ControlledByPlayer)
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

        internal static GameEntity[] GetFinancialCompanies(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholder)),
                e => e.company.CompanyType == CompanyType.FinancialGroup
            );
        }

        internal static GameEntity[] GetGroupCompanies(GameContext gameContext)
        {
            return Array.FindAll(
                gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholder)),
                e => IsManagingCompany(e)
            );
        }

        public static bool IsManagingCompany(GameEntity e)
        {
            var t = e.company.CompanyType;

            return t == CompanyType.Corporation || t == CompanyType.Group || t == CompanyType.Holding;
        }



        public static bool IsCompanyGroupLike(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            return IsCompanyGroupLike(c);
        }

        public static bool IsProductCompany(GameEntity gameEntity)
        {
            return gameEntity.company.CompanyType == CompanyType.ProductCompany;
        }

        public static bool IsProductCompany(GameContext context, int companyId)
        {
            return IsProductCompany(GetCompanyById(context, companyId));
        }

        public static bool IsCompanyGroupLike(GameEntity gameEntity)
        {
            return !IsProductCompany(gameEntity);
        }

        public static GameEntity[] GetInvestableCompanies(GameContext context)
        {
            return context.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));
        }

        public static GameEntity[] GetInvestments(GameContext context, int investorId)
        {
            return Array.FindAll(
                GetInvestableCompanies(context),
                c => c.shareholders.Shareholders.ContainsKey(investorId)
                );
        }

        public static int GetRandomInvestmentFund(GameContext context)
        {
            var funds = GetFinancialCompanies(context);

            var index = UnityEngine.Random.Range(0, funds.Length);

            return funds[index].shareholder.Id;
        }

        // phase
        public static long GetAudienceGrowth(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].AudienceSize + 1;
            var now = metrics[len - 1].AudienceSize + 1;

            return (now - was) * 100 / was;
        }

        public static long GetValuationGrowth(GameEntity e, int duration)
        {
            var metrics = e.metricsHistory.Metrics;

            if (metrics.Count < duration)
                return 0;

            var len = metrics.Count;

            var was = metrics[len - duration].Valuation + 1;
            var now = metrics[len - 1].Valuation + 1;

            return (now - was) * 100 / was;
        }

        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
