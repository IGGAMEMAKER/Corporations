using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static InvestorGoal GetInvestorGoal(GameContext context, int investorId)
        {
            return InvestorGoal.GrowCompanyCost;
        }

        public static int GetTotalShares(GameContext context, int companyId)
        {
            return GetTotalShares(GetCompanyById(context, companyId).shareholders.Shareholders);
        }

        public static int GetTotalShares(Dictionary<int, BlockOfShares> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value.amount;

            return totalShares;
        }

        public static bool IsSharesCanBeSold(GameEntity company)
        {
            return company.isPublicCompany || company.hasAcceptsInvestments;
        }

        public static bool IsAreSharesSellable(GameContext context, int companyId)
        {
            return IsSharesCanBeSold(GetCompanyById(context, companyId));
        }

        public static Dictionary<int, BlockOfShares> GetCompanyShares(GameEntity company)
        {
            var shareholders = new Dictionary<int, BlockOfShares>();

            if (company.hasShareholders)
                shareholders = company.shareholders.Shareholders;

            return shareholders;
        }

        public static int GetAmountOfShares(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            return shareholders.ContainsKey(investorId) ? shareholders[investorId].amount : 0;
        }

        public static int GetShareSize(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);

            int shares = GetAmountOfShares(context, companyId, investorId);
            int total = GetTotalShares(c.shareholders.Shareholders);

            return shares * 100 / total;
        }

        public static long GetSharesCost(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);

            int shares = GetAmountOfShares(context, companyId, investorId);
            int total = GetTotalShares(c.shareholders.Shareholders);

            return CompanyEconomyUtils.GetCompanyCost(context, c.company.Id) * shares / total;
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

        public static string GetInvestorName(GameEntity investor)
        {
            return investor.shareholder.Name;
        }

        public static string GetInvestorName(GameContext context, int investorId)
        {
            return GetInvestorName(GetInvestorById(context, investorId));
        }

        public static int GetBiggestShareholder(GameContext gameContext, int companyId)
        {
            var c = GetCompanyById(gameContext, companyId);

            var list = c.shareholders.Shareholders.OrderBy(key => key.Value);

            return list.First().Key;
        }

        public static string GetBiggestShareholderName(GameContext gameContext, int companyId)
        {
            return GetInvestorName(gameContext, GetBiggestShareholder(gameContext, companyId));
        }

        public static string GetShareholderStatus(int sharesPercent)
        {
            if (sharesPercent < 1)
                return "Non voting";

            if (sharesPercent < 10)
                return "Voting";

            if (sharesPercent < 25)
                return "Majority";

            if (sharesPercent < 50)
                return "Blocking";

            if (sharesPercent < 100)
                return "Controling";

            return "Owner";
        }
    }
}
