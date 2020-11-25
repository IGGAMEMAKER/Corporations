using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void AddShares(GameEntity company, GameEntity investor, int shares)
        {
            int investorId = investor.shareholder.Id;
            var block = new BlockOfShares
            {
                amount = shares,

                InvestorType = investor.shareholder.InvestorType,
                shareholderLoyalty = 100,

                Investments = new List<Investment>()
            };

            var shareholders = GetShareholders(company);

            BlockOfShares b1;

            if (IsInvestsInCompany(company, investor))
            {
                // increase shares
                b1 = shareholders[investorId];
                b1.amount += block.amount;
            }
            else
            {
                // set shares
                b1 = block;
            }

            shareholders[investorId] = b1;

            AddOwning(investor, company.company.Id);
            ReplaceShareholders(company, shareholders);
        }





        public static void DecreaseShares(GameContext gameContext, GameEntity company, int investorId, int shares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = Investments.GetInvestor(gameContext, investorId);

            var prev = shareholders[investorId];

            if (shares >= prev.amount)
            {
                // will lose all shares
                // needs to be deleted
                RemoveShareholder(company, gameContext, shareholder);

                return;
            }

            shareholders[investorId] = new BlockOfShares
            {
                amount = prev.amount - shares,
                InvestorType = prev.InvestorType,
                shareholderLoyalty = prev.shareholderLoyalty
            };

            ReplaceShareholders(company, shareholders);
        }

        public static void RemoveShareholder(GameEntity company, GameContext gameContext, GameEntity investor)
        {
            // remove from company
            var shareholders = company.shareholders.Shareholders;

            shareholders.Remove(investor.shareholder.Id);

            ReplaceShareholders(company, shareholders);

            // remove from shareholder
            RemoveOwning(investor, company.company.Id);
        }

        public static void TransferShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            Companies.Log(company, $"Buy {amountOfShares} shares of {company.company.Name} for ${bid}");
            Companies.Log(company, $"Buyer: {GetInvestorName(context, buyerInvestorId)}");
            Companies.Log(company, $"Seller: {GetInvestorName(context, sellerInvestorId)}");

            //AddShares2(context, company, buyerInvestorId, amountOfShares);
            AddShares(company, GetInvestorById(context, buyerInvestorId), amountOfShares);

            DecreaseShares(context, company, sellerInvestorId, amountOfShares);

            Companies.Log(company, "Transferred");
        }

        public static void RemoveAllShareholders(GameContext gameContext, GameEntity company)
        {
            //var shareholders = GetShareholders(company);
            //int[] array = new int[company.shareholders.Shareholders.Keys.Count];
            //shareholders.Keys.CopyTo(array, 0);

            // remove old shareholders
            foreach (var sellerInvestorId in GetShareholdersCopy(company))
            {
                RemoveShareholder(company, gameContext, GetInvestorById(gameContext, sellerInvestorId));
            }
        }

        public static void TransferCompany(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            RemoveAllShareholders(gameContext, company);

            // set new shareholder
            AddShares(company, buyer, 100);
        }

        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
        }
    }
}
