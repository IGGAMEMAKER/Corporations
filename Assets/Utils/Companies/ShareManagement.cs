using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int GetTotalShares(Dictionary<int, int> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value;

            return totalShares;
        }

        public static int GetAmountOfShares(GameContext context, int companyId, int investorId)
        {
            Debug.Log($"GetAmountOfShares company-{companyId}, investor-{GetInvestorById(context, investorId)}");
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



        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context); // GenerateInvestorId();

            string name = "Investor?";

            // company
            if (e.hasCompany)
                name = e.company.Name;

            // or human
            // TODO turn human to investor

            e.AddShareholder(investorId, name, money);

            return investorId;
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, int shares)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> shareholders;

            if (!c.hasShareholders)
            {
                shareholders = new Dictionary<int, int>();
                shareholders[investorId] = shares;

                c.AddShareholders(shareholders);
            }
            else
            {
                shareholders = c.shareholders.Shareholders;
                shareholders[investorId] = shares;

                c.ReplaceShareholders(shareholders);
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
                shareholders.Remove(sellerInvestorId);

            shareholders[buyerInvestorId] = newBuyerShares;

            c.ReplaceShareholders(shareholders);
        }

        public static string GetInvestorName(GameContext context, int investorId)
        {
            return GetInvestorById(context, investorId).shareholder.Name;
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

            investor.ReplaceShareholder(shareholder.Id, shareholder.Name, shareholder.Money + sum);
        }
    }
}
