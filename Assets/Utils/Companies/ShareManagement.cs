using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static bool IsAreSharesSellable(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (c.isPublicCompany || c.hasAcceptsInvestments)
                return true;

            return false;
        }

        public static Dictionary<int, BlockOfShares> GetCompanyShares(GameEntity company)
        {
            var shareholders = new Dictionary<int, BlockOfShares>();

            if (company.hasShareholders)
                shareholders = company.shareholders.Shareholders;

            return shareholders;
        }

        public static void StartInvestmentRound(GameContext gameContext, int companyId)
        {
            var c = GetCompanyById(gameContext, companyId);

            var round = EnumUtils.Next(c.investmentRounds.InvestmentRound);

            c.ReplaceInvestmentRounds(round);
            c.AddAcceptsInvestments(Constants.INVESTMENT_ROUND_ACTIVE_FOR_DAYS);
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

        public static string GetInvestorName(GameContext context, int investorId)
        {
            return GetInvestorById(context, investorId).shareholder.Name;
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

        // update
        public static void CopyShareholders(GameContext gameContext, int from, int to)
        {
            var cFrom = GetCompanyById(gameContext, from);
            var cTo = GetCompanyById(gameContext, to);

            ReplaceShareholders(cTo, cFrom.shareholders.Shareholders);
        }

        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
        }

        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context);

            string name = "Investor?";

            InvestorType investorType = InvestorType.VentureInvestor;

            // company
            if (e.hasCompany)
            {
                name = e.company.Name;

                if (e.company.CompanyType == CompanyType.FinancialGroup)
                    investorType = InvestorType.VentureInvestor;
                else
                    investorType = InvestorType.Strategic;
            }
            else if (e.hasHuman)
            {
                // or human
                // TODO turn human to investor

                name = e.human.Name + " " + e.human.Surname;
                investorType = InvestorType.Founder;
            }

            e.AddShareholder(investorId, name, investorType);
            AddMoneyToInvestor(context, investorId, money);

            return investorId;
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, BlockOfShares block)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            BlockOfShares b;

            if (shareholders.ContainsKey(investorId))
            {
                b = shareholders[investorId];
                b.amount += block.amount;
            }
            else
            {
                b = block;
            }

            shareholders[investorId] = b;

            ReplaceShareholders(c, shareholders);
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, int shares)
        {
            var b = new BlockOfShares
            {
                amount = shares,
                
                // no time limit
                expires = -1,
                InvestorGoal = GetInvestorGoal(context, investorId),
                shareholderLoyalty = 100
            };

            AddShareholder(context, companyId, investorId, b);
        }

        public static void TransferShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            int newSellerSharesAmount = GetAmountOfShares(context, companyId, sellerInvestorId) - amountOfShares;

            var SellerBlockOfShares = shareholders[sellerInvestorId];
            SellerBlockOfShares.amount = newSellerSharesAmount;

            if (newSellerSharesAmount == 0)
                shareholders.Remove(sellerInvestorId);


            var BuyerBlockOfShares = shareholders[buyerInvestorId];
            BuyerBlockOfShares.amount = GetAmountOfShares(context, companyId, buyerInvestorId) + amountOfShares;

            ReplaceShareholders(c, shareholders);
        }

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            Debug.Log($"Buy {amountOfShares} shares of {companyId} for ${bid}");
            Debug.Log($"Buyer: {GetInvestorName(context, buyerInvestorId)}");
            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            TransferShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares);

            AddMoneyToInvestor(context, buyerInvestorId, -bid);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            var investor = GetInvestorById(context, investorId);

            var companyResource = investor.companyResource;
            companyResource.Resources.AddMoney(sum);

            investor.ReplaceCompanyResource(companyResource.Resources);
        }
    }
}
