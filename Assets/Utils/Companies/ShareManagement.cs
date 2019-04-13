using System;
using System.Collections.Generic;

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
            return GetCompanyById(context, companyId).shareholders.Shareholders[investorId];
        }

        public static int GetShareSize(GameContext context, int companyId, int investorId)
        {
            return GetAmountOfShares(context, companyId, investorId) * 100 / GetTotalShares(GetCompanyById(context, companyId).shareholders.Shareholders);
        }

        public static long GetSharesCost(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);
            int amountOfShares = c.shareholders.Shareholders[investorId];

            int totalShares = GetTotalShares(c.shareholders.Shareholders);

            return GetCompanyCost(context, c.company.Id) * amountOfShares / totalShares;
        }

        internal static long GetCompanyCost(GameContext gameContext, int companyId)
        {
            return 500000;
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

            if (shareholders.TryGetValue(buyerInvestorId, out int newBuyerShares))
                newBuyerShares += amountOfShares;

            if (shareholders.TryGetValue(buyerInvestorId, out int newSellerShares))
                newSellerShares -= amountOfShares;

            shareholders[sellerInvestorId] = newSellerShares;
            if (newSellerShares == 0)
                shareholders.Remove(sellerInvestorId);

            shareholders[buyerInvestorId] = newBuyerShares;

            c.ReplaceShareholders(shareholders);
        }

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
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
