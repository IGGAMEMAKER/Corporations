using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void BuyShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long offer, bool comparedToShareSize)
        {
            var shareSize = GetShareSize(context, company, sellerInvestorId);
            BuyShares(context, company, buyerInvestorId, sellerInvestorId, amountOfShares, offer * shareSize / 100);
        }

        public static void BuyShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            long bid = GetSharesCost(context, company, sellerInvestorId);
            BuyShares(context, company, buyerInvestorId, sellerInvestorId, amountOfShares, bid);
        }

        public static void BuyShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            // protecting from buying your own shares
            if (buyerInvestorId == sellerInvestorId)
                return;

            // buy all
            if (amountOfShares == -1)
                amountOfShares = GetAmountOfShares(context, company, sellerInvestorId);

            if (company.hasShareholder && buyerInvestorId == company.shareholder.Id)
            {
                BuyBack(context, company, sellerInvestorId, amountOfShares);
                return;
            }

            Debug.Log($"Buy {amountOfShares} shares of {company.company.Name} for ${bid}");
            Debug.Log($"Buyer: {GetInvestorName(context, buyerInvestorId)}");
            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            TransferShares(context, company, buyerInvestorId, sellerInvestorId, amountOfShares);

            Debug.Log("Transferred");
            GetMoneyFromInvestor(context, buyerInvestorId, bid);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }

        public static int GetPortionSize(GameContext gameContext, GameEntity company, int sellerInvestorId, int percent)
        {
            var shares = GetAmountOfShares(gameContext, company, sellerInvestorId);

            var totalShares = GetTotalShares(company.shareholders.Shareholders);

            var portion = totalShares * percent / 100;

            if (portion > shares)
                portion = shares;

            return portion;
        }

        public static void BuyBackPercent(GameContext context, GameEntity company, int sellerInvestorId, int percent)
        {
            var portion = GetPortionSize(context, company, sellerInvestorId, percent);

            Debug.Log($"Buy back percent: {portion}");

            BuyBack(context, company, sellerInvestorId, portion);
        }

        public static void BuyBack(GameContext context, GameEntity company, int sellerInvestorId, int amountOfShares)
        {
            var bid = GetSharesCost(context, company, sellerInvestorId, amountOfShares);

            Debug.Log($"Buy Back! {amountOfShares} shares of {company.company.Name} for ${bid}");
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
