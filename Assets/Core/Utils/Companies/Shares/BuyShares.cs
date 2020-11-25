using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void BuyShares(GameContext context, GameEntity company, GameEntity buyer, GameEntity seller, int amountOfShares, long offer, bool comparedToShareSize)
        {
            var shareSize = GetShareSize(context, company, seller);

            BuyShares(context, company, buyer, seller, amountOfShares, offer * shareSize / 100);
        }
        public static void BuyShares(GameContext context, GameEntity company, GameEntity buyer, GameEntity seller, int amountOfShares, long bid)
        {
            int sellerInvestorId = seller.shareholder.Id;
            int buyerInvestorId = buyer.shareholder.Id;

            // protecting from buying your own shares
            if (buyerInvestorId == sellerInvestorId)
                return;

            // buy all
            if (amountOfShares == -1)
                amountOfShares = GetAmountOfShares(context, company, seller);

            if (company.hasShareholder && buyerInvestorId == company.shareholder.Id)
            {
                BuyBack(context, company, seller, amountOfShares);
                return;
            }

            TransferShares(context, company, buyer, seller, amountOfShares, bid);


            SpendResources(buyer, bid, "Buy Shares of " + company.company.Name);
            AddResources(seller, bid, "Buy Shares of " + company.company.Name);
        }

        public static int GetPortionSize(GameContext gameContext, GameEntity company, GameEntity seller, int percent)
        {
            var shares = GetAmountOfShares(gameContext, company, seller);

            var totalShares = GetTotalShares(company);

            var portion = totalShares * percent / 100;

            if (portion > shares)
                portion = shares;

            return portion;
        }

        public static void BuyBackPercent(GameContext context, GameEntity company, GameEntity sellerInvestorId, int percent)
        {
            var portion = GetPortionSize(context, company, sellerInvestorId, percent);

            Debug.Log($"Buy back percent: {portion}");

            BuyBack(context, company, sellerInvestorId, portion);
        }

        public static void BuyBack(GameContext context, GameEntity company, GameEntity seller, int amountOfShares)
        {
            // TODO BUYBACK
            var sellerInvestorId = seller.shareholder.Id;
            var bid = GetSharesCost(context, company, seller, amountOfShares);

            Debug.Log($"Buy Back! {amountOfShares} shares of {company.company.Name} for ${bid}");
            var cost = bid;

            if (!IsEnoughResources(company, cost))
                return;

            Debug.Log($"Seller: {GetInvestorName(seller)}");

            var b = company.shareholders.Shareholders[sellerInvestorId]; //.amount -= amountOfShares;

            b.amount -= amountOfShares;

            company.shareholders.Shareholders[sellerInvestorId] = b;

            if (b.amount <= 0)
                RemoveShareholder(company, context, seller);

            Companies.SpendResources(company, cost, "Buy back");
            Companies.AddResources(seller, bid, "Buy back");
        }
    }
}
