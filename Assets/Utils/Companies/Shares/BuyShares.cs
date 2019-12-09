using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
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

            if (amountOfShares == -1)
                amountOfShares = GetAmountOfShares(context, companyId, sellerInvestorId);

            var c = GetCompany(context, companyId);
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


        public static void BuyBack(GameContext context, GameEntity company, int companyId, int sellerInvestorId, int amountOfShares)
        {
            var bid = GetSharesCost(context, companyId, sellerInvestorId);

            Debug.Log($"Buy Back! {amountOfShares} shares of {companyId} for ${bid}");
            var cost = new Classes.TeamResource(bid);

            if (!IsEnoughResources(company, cost))
                return;

            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            RemoveShareholder(company, sellerInvestorId);

            SpendResources(company, cost);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }
    }
}
