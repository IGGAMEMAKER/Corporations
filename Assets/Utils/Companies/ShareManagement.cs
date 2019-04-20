using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int GetTotalShares(GameContext context, int companyId)
        {
            return GetTotalShares(GetCompanyById(context, companyId).shareholders.Shareholders);
        }

        public static int GetTotalShares(Dictionary<int, int> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value;

            return totalShares;
        }

        public static bool IsAreSharesSellable(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (c.investmentRounds.IsPublic || c.investmentRounds.IsActive)
                return true;

            return false;
        }

        public static void StartInvestmentRound(GameContext gameContext, int companyId)
        {
            if (!IsAreSharesSellable(gameContext, companyId))
                return;

            var c = GetCompanyById(gameContext, companyId);

            var round = EnumUtils.Next(c.investmentRounds.InvestmentRound);

            c.ReplaceInvestmentRounds(c.investmentRounds.IsPublic, round, Constants.INVESTMENT_ROUND_ACTIVE_FOR_DAYS, true);
        }

        public static int GetAmountOfShares(GameContext context, int companyId, int investorId)
        {
            //Debug.Log($"GetAmountOfShares company-{companyId}, investor-{GetInvestorById(context, investorId)}");
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            return shareholders.ContainsKey(investorId) ? shareholders[investorId] : 0;
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

            return GetCompanyCost(context, c.company.Id) * shares / total;
        }

        internal static long GetCompanyCost(GameContext gameContext, int companyId)
        {
            return CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);
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


        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context); // GenerateInvestorId();

            string name = "Investor?";

            InvestorType investorType = InvestorType.VentureInvestor;

            // company
            if (e.hasCompany)
            {
                name = e.company.Name;

                if (e.company.CompanyType == CompanyType.FinancialGroup)
                {
                    investorType = InvestorType.VentureInvestor;
                }
                else
                {
                    investorType = InvestorType.Strategic;
                }
            }
            else if (e.hasHuman)
            {
                name = e.human.Name + " " + e.human.Surname;
                investorType = InvestorType.Founder;
            }

            // or human
            // TODO turn human to investor

            e.AddShareholder(investorId, name, investorType);
            AddMoneyToInvestor(context, investorId, money);

            return investorId;
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, int shares)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> shareholders;
            Dictionary<int, InvestorGoal> goals;

            InvestorGoal goal = GetInvestorGoal(context, investorId);

            if (!c.hasShareholders)
            {
                shareholders = new Dictionary<int, int>
                {
                    [investorId] = shares
                };
                goals = new Dictionary<int, InvestorGoal>
                {
                    [investorId] = goal
                };

                c.AddShareholders(shareholders, goals);
            }
            else
            {
                shareholders = c.shareholders.Shareholders;
                goals = c.shareholders.Goals;

                if (shareholders.ContainsKey(investorId))
                {
                    shareholders[investorId] += shares;
                }
                else
                {
                    shareholders[investorId] = shares;
                    goals[investorId] = goal;
                }

                c.ReplaceShareholders(shareholders, goals);
            }
        }

        public static void TransferShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            int newBuyerShares = GetAmountOfShares(context, companyId, buyerInvestorId) + amountOfShares;
            int newSellerShares = GetAmountOfShares(context, companyId, sellerInvestorId) - amountOfShares;

            shareholders[sellerInvestorId] = newSellerShares;
            if (newSellerShares == 0)
            {
                shareholders.Remove(sellerInvestorId);
                c.shareholders.Goals.Remove(sellerInvestorId);
            }

            shareholders[buyerInvestorId] = newBuyerShares;

            c.ReplaceShareholders(shareholders, c.shareholders.Goals);
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

            var shareholder = investor.shareholder;

            var companyResource = investor.companyResource;
            companyResource.Resources.AddMoney(sum);

            investor.ReplaceCompanyResource(companyResource.Resources);
        }
    }
}
