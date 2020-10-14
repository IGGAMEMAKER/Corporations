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

        // Add shareholder

        public static void AddShareholder(GameContext context, GameEntity company, int investorId, int shares)
        {
            AddShareholder(company, GetInvestorById(context, investorId), shares);
        }
        public static void AddShareholder(GameEntity company, GameEntity investor, int shares)
        {
            var b = new BlockOfShares
            {
                amount = shares,
                
                InvestorType = investor.shareholder.InvestorType,
                shareholderLoyalty = 100,

                Investments = new List<Investment>()
            };

            AddShareholder(company, investor.shareholder.Id, b);
        }
        public static void AddShareholder(GameEntity c, int investorId, BlockOfShares block)
        {
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
        //


        public static void AddShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var inv = Investments.GetInvestor(gameContext, investorId);
            var shareholder = inv.shareholder;

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
                    InvestorType = shareholder.InvestorType,
                    shareholderLoyalty = 100,
                };
            }

            AddOwning(inv, company.company.Id);
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



        public static void DecreaseShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = Investments.GetInvestor(gameContext, investorId).shareholder;

            var prev = shareholders[investorId];

            if (amountOfShares >= prev.amount)
            {
                // needs to be deleted
                RemoveShareholder(company, gameContext, investorId);
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

        public static void TransferShares(GameContext context, GameEntity company, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            AddShares(context, company, buyerInvestorId, amountOfShares);
            DecreaseShares(context, company, sellerInvestorId, amountOfShares);
        }




        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            Investments.AddMoneyToInvestor(context, investorId, sum);
        }

        public static void GetMoneyFromInvestor(GameContext gameContext, int investorId, long sum)
        {
            AddMoneyToInvestor(gameContext, investorId, -sum);
        }
    }
}
