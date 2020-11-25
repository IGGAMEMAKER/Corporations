using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
        }

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

                shareholders[investorId] = b1;
            }
            else
            {
                // set shares
                b1 = block;

                shareholders[investorId] = b1;
            }

            AddOwning(investor, company.company.Id);
            ReplaceShareholders(company, shareholders);
        }


        public static void AddShares2(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var investor = Investments.GetInvestor(gameContext, investorId);

            if (IsInvestsInCompany(company, investorId))
            {
                var prev = shareholders[investorId];

                shareholders[investorId] = new BlockOfShares
                {
                    amount = prev.amount + amountOfShares,
                    InvestorType = prev.InvestorType,
                    shareholderLoyalty = prev.shareholderLoyalty,
                };
            }
            else
            {
                // new investor
                shareholders[investorId] = new BlockOfShares
                {
                    amount = amountOfShares,
                    InvestorType = investor.shareholder.InvestorType,
                    shareholderLoyalty = 100,
                };
            }

            AddOwning(investor, company.company.Id);
            ReplaceShareholders(company, shareholders);
        }

        public static void RemoveShareholder(GameEntity company, GameContext gameContext, int shareholderId)
        {
            var shareholders = company.shareholders.Shareholders;

            shareholders.Remove(shareholderId);

            ReplaceShareholders(company, shareholders);

            // remove owning from shareholder
            var shareholder = GetInvestorById(gameContext, shareholderId);
            shareholder.ownings.Holdings.Remove(company.company.Id);
        }



        public static void DecreaseShares(GameContext gameContext, GameEntity company, int investorId, int shares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = Investments.GetInvestor(gameContext, investorId).shareholder;

            var prev = shareholders[investorId];

            if (shares >= prev.amount)
            {
                // will lose all shares
                // needs to be deleted
                RemoveShareholder(company, gameContext, investorId);

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

        public static void DestroyBlockOfShares(GameContext gameContext, GameEntity company, int investorId)
        {
            var shareholders = company.shareholders.Shareholders;

            shareholders.Remove(investorId);

            ReplaceShareholders(company, shareholders);
        }

        public static void TransferShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            //AddShares2(context, company, buyerInvestorId, amountOfShares);
            AddShares(company, GetInvestorById(context, buyerInvestorId), amountOfShares);

            DecreaseShares(context, company, sellerInvestorId, amountOfShares);
        }

        public static void TransferCompany(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var shareholders = GetShareholders(company);
            int[] array = new int[company.shareholders.Shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            // remove old shareholders
            foreach (var sellerInvestorId in array)
            {
                RemoveShareholder(company, gameContext, sellerInvestorId);
            }

            // set new shareholder
            AddShares(company, buyer, 100);
        }
    }
}
