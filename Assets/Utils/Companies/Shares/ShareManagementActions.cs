using Assets.Utils.Formatting;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        // update
        public static void CopyShareholders(GameContext gameContext, int from, int to)
        {
            var cFrom = GetCompanyById(gameContext, from);
            var cTo = GetCompanyById(gameContext, to);

            ReplaceShareholders(cTo, cFrom.shareholders.Shareholders);
        }

        public static void StartInvestmentRound(GameContext gameContext, int companyId)
        {
            StartInvestmentRound(GetCompanyById(gameContext, companyId));
        }

        public static void StartInvestmentRound(GameEntity company)
        {
            var round = EnumUtils.Next(company.investmentRounds.InvestmentRound);

            company.ReplaceInvestmentRounds(round);
            company.AddAcceptsInvestments(Constants.INVESTMENT_ROUND_ACTIVE_FOR_DAYS);
        }

        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
        }

        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            return InvestmentUtils.BecomeInvestor(context, e, money);
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
            InvestmentUtils.AddMoneyToInvestor(context, investorId, sum);
        }
    }
}
