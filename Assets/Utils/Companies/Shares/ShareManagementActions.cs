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
            StartInvestmentRound(GetCompanyById(gameContext, companyId), gameContext);
        }

        public static InvestmentRound GetInvestmentRoundName(GameEntity company)
        {
            switch (company.companyGoal.InvestorGoal)
            {
                case InvestorGoal.Prototype:
                    return InvestmentRound.Preseed;

                case InvestorGoal.FirstUsers:
                    return InvestmentRound.Seed;

                case InvestorGoal.BecomeMarketFit:
                    return InvestmentRound.A;

                case InvestorGoal.Release:
                    return InvestmentRound.B;

                case InvestorGoal.BecomeProfitable:
                    return InvestmentRound.C;

                case InvestorGoal.GrowCompanyCost:
                    return InvestmentRound.C;

                case InvestorGoal.IPO:
                    return InvestmentRound.D;

                default:
                    return InvestmentRound.E;
            }
        }

        public static void NotifyAboutInvestmentRound(GameEntity company, GameContext gameContext)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (IsInSphereOfInterest(playerCompany, company))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageInvestmentRoundStarted(company.company.Id));
        }

        public static void StartInvestmentRound(GameEntity company, GameContext gameContext)
        {
            if (company.hasAcceptsInvestments)
                return;

            NotifyAboutInvestmentRound(company, gameContext);

            var round = GetInvestmentRoundName(company);

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
                
                InvestorType = GetInvestorById(context, investorId).shareholder.InvestorType,
                shareholderLoyalty = 100
            };

            AddShareholder(context, companyId, investorId, b);
        }

        public static void AddShares(GameContext gameContext, GameEntity company, int investorId, int amountOfShares)
        {
            var shareholders = company.shareholders.Shareholders;
            var shareholder = InvestmentUtils.GetInvestorById(gameContext, investorId).shareholder;

            if (!IsInvestsInCompany(company, investorId))
            {
                // new investor
                shareholders[investorId] = new BlockOfShares
                {
                    amount = amountOfShares,
                    shareholderLoyalty = 100,
                    InvestorType = shareholder.InvestorType
                };
            } else
            {
                var prev = shareholders[investorId];

                shareholders[investorId] = new BlockOfShares
                {
                    amount = prev.amount + amountOfShares,
                    InvestorType = prev.InvestorType,
                    shareholderLoyalty = prev.shareholderLoyalty
                };
            }

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
                shareholders.Remove(investorId);
            } else
            {
                shareholders[investorId] = new BlockOfShares
                {
                    amount = prev.amount - amountOfShares,
                    InvestorType = prev.InvestorType,
                    shareholderLoyalty = prev.shareholderLoyalty
                };
            }

            ReplaceShareholders(company, shareholders);
        }

        public static void TransferShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            var c = GetCompanyById(context, companyId);

            AddShares(context, c, buyerInvestorId, amountOfShares);
            DecreaseShares(context, c, sellerInvestorId, amountOfShares);
        }

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            Debug.Log($"Buy {amountOfShares} shares of {companyId} for ${bid}");
            Debug.Log($"Buyer: {GetInvestorName(context, buyerInvestorId)}");
            Debug.Log($"Seller: {GetInvestorName(context, sellerInvestorId)}");

            TransferShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares);

            Debug.Log("Transferred");
            AddMoneyToInvestor(context, buyerInvestorId, -bid);
            AddMoneyToInvestor(context, sellerInvestorId, bid);
        }

        public static void PayDividends(GameContext gameContext, int companyId)
        {
            PayDividends(gameContext, GetCompanyById(gameContext, companyId));
        }

        public static void PayDividends(GameContext gameContext, GameEntity company)
        {
            int dividendSize = 33;
            var balance = company.companyResource.Resources.money;

            var dividends = balance * dividendSize / 100;

            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                var sharePercentage = GetShareSize(gameContext, company.company.Id, investorId);

                var sum = dividends * sharePercentage / 100;

                AddMoneyToInvestor(gameContext, investorId, sum);
            }

            SpendResources(company, new Classes.TeamResource(dividends));
        }

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            InvestmentUtils.AddMoneyToInvestor(context, investorId, sum);
        }
    }
}
