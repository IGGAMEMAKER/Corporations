using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long offer, bool comparedToShareSize)
        {
            var shareSize = GetShareSize(context, companyId, sellerInvestorId);
            BuyShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares, offer * shareSize / 100);
        }

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            long bid = GetSharesCost(context, companyId, sellerInvestorId);
            BuyShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares, bid);
        }

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            // protecting from buying your own shares
            if (buyerInvestorId == sellerInvestorId)
                return;

            // buy all
            if (amountOfShares == -1)
                amountOfShares = GetAmountOfShares(context, companyId, sellerInvestorId);

            var c = Get(context, companyId);
            if (c.hasShareholder && buyerInvestorId == c.shareholder.Id)
            {
                BuyBack(context, c, companyId, sellerInvestorId, amountOfShares);
                return;
            }

            Debug.Log($"Buy {amountOfShares} shares of {companyId} for ${bid}");
            Debug.Log($"Buyer: {GetInvestorName(context, buyerInvestorId)}");
            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            TransferShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares);

            Debug.Log("Transferred");
            GetMoneyFromInvestor(context, buyerInvestorId, bid);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }

        public static int GetPortionSize(GameContext gameContext, GameEntity company, int companyId, int sellerInvestorId, int percent)
        {
            var shares = GetAmountOfShares(gameContext, companyId, sellerInvestorId);

            var totalShares = GetTotalShares(company.shareholders.Shareholders);

            var portion = totalShares * percent / 100;

            if (portion > shares)
                portion = shares;

            return portion;
        }

        public static void BuyBackPercent(GameContext context, GameEntity company, int companyId, int sellerInvestorId, int percent)
        {
            var portion = GetPortionSize(context, company, companyId, sellerInvestorId, percent);

            Debug.Log($"Buy back percent: {portion}");

            BuyBack(context, company, companyId, sellerInvestorId, portion);
        }

        public static void BuyBack(GameContext context, GameEntity company, int companyId, int sellerInvestorId, int amountOfShares)
        {
            var bid = GetSharesCost(context, companyId, sellerInvestorId, amountOfShares);

            Debug.Log($"Buy Back! {amountOfShares} shares of {companyId} for ${bid}");
            var cost = bid;

            if (!IsEnoughResources(company, cost))
                return;

            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            var b = company.shareholders.Shareholders[sellerInvestorId]; //.amount -= amountOfShares;

            b.amount -= amountOfShares;

            company.shareholders.Shareholders[sellerInvestorId] = b;

            if (b.amount <= 0)
                RemoveShareholder(company, context, sellerInvestorId);

            SpendResources(company, cost);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }
    }
}
