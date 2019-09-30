using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, BlockOfShares block)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            BlockOfShares b;

            if (IsInvestsInCompany(c, investorId))
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
                
                InvestorType = GetInvestorById(context, investorId).shareholder.InvestorType,
                shareholderLoyalty = 100
            };

            AddShareholder(context, companyId, investorId, b);
        }

        public static void AddShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = InvestmentUtils.GetInvestorById(gameContext, investorId).shareholder;

            if (IsInvestsInCompany(company, investorId))
            {
                var prev = shareholders[investorId];

                shareholders[investorId] = new BlockOfShares
                {
                    amount = prev.amount + amountOfShares,
                    InvestorType = prev.InvestorType,
                    shareholderLoyalty = prev.shareholderLoyalty
                };
            }
            else
            {
                // new investor
                shareholders[investorId] = new BlockOfShares
                {
                    amount = amountOfShares,
                    shareholderLoyalty = 100,
                    InvestorType = shareholder.InvestorType
                };
            }

            ReplaceShareholders(company, shareholders);
        }

        public static void RemoveShareholder(GameEntity company, int shareholderId)
        {
            var shareholders = company.shareholders.Shareholders;

            shareholders.Remove(shareholderId);

            ReplaceShareholders(company, shareholders);
        }

        public static void DecreaseShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = InvestmentUtils.GetInvestorById(gameContext, investorId).shareholder;

            var prev = shareholders[investorId];

            if (amountOfShares >= prev.amount)
            {
                // needs to be deleted
                RemoveShareholder(company, investorId);
                return;
            }

            shareholders[investorId] = new BlockOfShares
            {
                amount = prev.amount - amountOfShares,
                InvestorType = prev.InvestorType,
                shareholderLoyalty = prev.shareholderLoyalty
            };

            ReplaceShareholders(company, shareholders);
        }

        public static void DestroyBlockOfShares(GameContext gameContext, GameEntity company, int investorId)
        {
            var shareholders = company.shareholders.Shareholders;

            shareholders.Remove(investorId);

            ReplaceShareholders(company, shareholders);
        }

        public static void TransferShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            var c = GetCompanyById(context, companyId);

            AddShares(context, c, buyerInvestorId, amountOfShares);
            DecreaseShares(context, c, sellerInvestorId, amountOfShares);
        }

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

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            // protecting from buying your own shares
            if (buyerInvestorId == sellerInvestorId)
                return;

            if (amountOfShares == -1)
                amountOfShares = GetAmountOfShares(context, companyId, sellerInvestorId);

            var c = GetCompanyById(context, companyId);
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



        public static void BuyCompany(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var salePrice = EconomyUtils.GetCompanySellingPrice(gameContext, companyId);

            BuyCompany(gameContext, companyId, buyerInvestorId, salePrice);
        }

        public static void BuyCompany(GameContext gameContext, int companyId, int buyerInvestorId, long offer)
        {
            // can afford acquisition
            var inv = InvestmentUtils.GetInvestorById(gameContext, buyerInvestorId);
            if (!inv.companyResource.Resources.IsEnoughResources(new Classes.TeamResource(offer)))
                return;

            var c = GetCompanyById(gameContext, companyId);

            var shareholders = c.shareholders.Shareholders;
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            foreach (var shareholderId in array)
                BuyShares(gameContext, companyId, buyerInvestorId, shareholderId, shareholders[shareholderId].amount, offer, true);

            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            c.isIndependentCompany = false;

            NotifyAboutAcquisition(gameContext, buyerInvestorId, companyId, offer);
        }

        public static void NotifyAboutAcquisition(GameContext gameContext, int buyerShareholderId, int targetCompanyId, long bid)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, buyerShareholderId, bid));
            
            Debug.LogFormat("ACQUISITION: {0} bought {1} for insane {2}!",
                GetInvestorName(gameContext, buyerShareholderId),
                GetCompanyById(gameContext, targetCompanyId).company.Name,
                Format.Money(bid));
        }

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            InvestmentUtils.AddMoneyToInvestor(context, investorId, sum);
        }

        public static void GetMoneyFromInvestor(GameContext gameContext, int investorId, long sum)
        {
            AddMoneyToInvestor(gameContext, investorId, -sum);
        }
    }
}
