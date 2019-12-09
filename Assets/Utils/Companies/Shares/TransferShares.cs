using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void ReplaceShareholders(GameEntity company, Dictionary<int, BlockOfShares> shareholders)
        {
            company.ReplaceShareholders(shareholders);
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
        public static void AddShareholder(GameContext context, int companyId, int investorId, BlockOfShares block)
        {
            var c = GetCompany(context, companyId);

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


        public static void AddShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = Investments.GetInvestorById(gameContext, investorId).shareholder;

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
            var shareholder = Investments.GetInvestorById(gameContext, investorId).shareholder;

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
            var c = GetCompany(context, companyId);

            AddShares(context, c, buyerInvestorId, amountOfShares);
            DecreaseShares(context, c, sellerInvestorId, amountOfShares);
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
